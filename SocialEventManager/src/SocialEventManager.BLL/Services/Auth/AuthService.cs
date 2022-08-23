using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SocialEventManager.Infrastructure.Auth;
using SocialEventManager.Infrastructure.Email;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Models.Auth;
using SocialEventManager.Shared.Models.Email;

namespace SocialEventManager.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailService _emailService;

    public AuthService(
        IConfiguration config,
        IJwtHandler jwtHandler,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailService emailService)
    {
        _config = config;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userRegistration)
    {
        ApplicationUser user = _mapper.Map<ApplicationUser>(userRegistration);
        IdentityResult result = await _userManager.CreateAsync(user, userRegistration.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }

        return result;
    }

    public async Task<(UserLoginResult Result, string? Token)> LoginAsync(UserLoginDto userLogin)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(userLogin.Email);
        if (user is null)
        {
            return (UserLoginResult.EmailNotFound, null);
        }

        SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, userLogin.Password, true, true);
        UserLoginResult userLoginResult = signInResult.ToUserLoginResult();

        string? token = userLoginResult == UserLoginResult.Success
            ? _jwtHandler.GenerateToken(userLogin.Email)
            : null;

        return (userLoginResult, token);
    }

    public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPassword)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(forgotPassword.Email);
        if (user is null)
        {
            return;
        }

        string url = await GenerateResetPasswordUrl(forgotPassword.Email, user);

        EmailDto email = new(AuthConstants.ForgotPasswordSubject, AuthConstants.ForgotPasswordBody(user.FirstName, url), new[] { forgotPassword.Email });
        BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(email));
    }

    public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPassword)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(resetPassword.Email);
        if (user is null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = nameof(AuthConstants.ResetPasswordFailed),
                Description = AuthConstants.ResetPasswordFailed,
            });
        }

        string token = resetPassword.Token.Decode();
        return await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
    }

    private async Task<string> GenerateResetPasswordUrl(string email, ApplicationUser user)
    {
        string validToken = await GenerateEncodedToken(user);
        return $"{_config[ConfigurationConstants.AppUrl]}/{ApiPathConstants.ResetPassword}?email={email}&token={validToken}";
    }

    private async Task<string> GenerateEncodedToken(ApplicationUser user)
    {
        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        return token.Encode();
    }
}
