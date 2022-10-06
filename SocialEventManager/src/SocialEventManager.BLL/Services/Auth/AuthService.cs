using Hangfire;
using LanguageExt.Common;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SocialEventManager.BLL.Services.Email;
using SocialEventManager.Infrastructure.Auth;
using SocialEventManager.Infrastructure.Email;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Models.Auth;
using SocialEventManager.Shared.Models.Email;

namespace SocialEventManager.BLL.Services.Auth;

public sealed class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailService _emailService;
    private readonly IEmailRenderer _renderer;

    public AuthService(
        IConfiguration config,
        IJwtHandler jwtHandler,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailService emailService,
        IEmailRenderer renderer)
    {
        _config = config;
        _renderer = renderer;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    public async Task<Result<bool>> RegisterUserAsync(UserRegistrationDto userRegistration)
    {
        ApplicationUser user = _mapper.Map<ApplicationUser>(userRegistration);
        IdentityResult result = await _userManager.CreateAsync(user, userRegistration.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
            await SendConfirmEmail(userRegistration);
        }

        return result.ToResult();
    }

    public async Task<Result<bool>> ConfirmEmailAsync(ConfirmEmailDto confirmEmail)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(confirmEmail.Email);
        if (user is null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = nameof(AuthConstants.ConfirmEmailFailed),
                Description = AuthConstants.ConfirmEmailFailed,
            }).ToResult();
        }

        string token = confirmEmail.Token.Decode();
        return (await _userManager.ConfirmEmailAsync(user, token)).ToResult();
    }

    public async Task<Result<string?>> LoginAsync(UserLoginDto userLogin)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(userLogin.Email);
        if (user is null)
        {
            return new Result<string?>(new UnauthorizedAccessException(UserLoginResult.EmailNotFound.GetDescription(), null));
        }

        if (!user.EmailConfirmed)
        {
            return new Result<string?>(new UnauthorizedAccessException(UserLoginResult.EmailNotVerified.GetDescription(), null));
        }

        SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, userLogin.Password, true, true);
        UserLoginResult userLoginResult = signInResult.ToUserLoginResult();

        return userLoginResult == UserLoginResult.Success
            ? _jwtHandler.GenerateToken(userLogin.Email)
            : new Result<string?>(new UnauthorizedAccessException(userLoginResult.GetDescription()));
    }

    public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPassword)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(forgotPassword.Email);
        if (user is null)
        {
            return;
        }

        string url = await GenerateResetPasswordUrl(user);
        string html = await _renderer.RenderAsync(new ForgotPassword(user.FirstName, url));

        EmailDto email = new(EmailConstants.ForgotPasswordSubject, html, new[] { forgotPassword.Email });
        BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(email));
    }

    public async Task<Result<bool>> ResetPasswordAsync(ResetPasswordDto resetPassword)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(resetPassword.Email);
        if (user is null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = nameof(AuthConstants.ResetPasswordFailed),
                Description = AuthConstants.ResetPasswordFailed,
            }).ToResult();
        }

        string token = resetPassword.Token.Decode();
        return (await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword)).ToResult();
    }

    private async Task SendConfirmEmail(UserRegistrationDto userRegistration)
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(userRegistration.Email);
        string url = await GenerateConfirmEmailUrl(user);
        string html = await _renderer.RenderAsync(new VerifyEmail(user.FirstName, url));

        EmailDto email = new(EmailConstants.VerifyEmailSubject, html, new[] { userRegistration.Email });
        BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(email));
    }

    private async Task<string> GenerateConfirmEmailUrl(ApplicationUser user)
    {
        string validToken = await GenerateEncodedEmailConfirmationToken(user);
        return $"{_config[ConfigurationConstants.AppUrl]}/{ApiPathConstants.ConfirmEmail}?email={user.Email}&token={validToken}";
    }

    private async Task<string> GenerateEncodedEmailConfirmationToken(ApplicationUser user)
    {
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return token.Encode();
    }

    private async Task<string> GenerateResetPasswordUrl(ApplicationUser user)
    {
        string validToken = await GenerateEncodedPasswordResetToken(user);
        return $"{_config[ConfigurationConstants.AppUrl]}/{ApiPathConstants.ResetPassword}?email={user.Email}&token={validToken}";
    }

    private async Task<string> GenerateEncodedPasswordResetToken(ApplicationUser user)
    {
        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        return token.Encode();
    }
}
