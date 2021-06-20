using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.DAL.Enums;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.API.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using SocialEventManager.BLL.Models.Users;

    [ApiController]
    [Route("api/account")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountsController> _logger;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ILogger<AccountsController> logger, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Register(RegisterUserDto user)
        {
            ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(user);
            IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);

            if (!result.Succeeded)
            {
                string errorMessage = result.Errors.ToErrorMessage();
                _logger.LogInformation(errorMessage);

                return BadRequest(errorMessage);
            }

            ApplicationUser currentUser = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(currentUser, RoleType.User.GetDescription());

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, isPersistent: true, lockoutOnFailure: false);

            return result.IsLockedOut
                ? Unauthorized()
                : result.Succeeded
                    ? Ok()
                    : BadRequest();
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
