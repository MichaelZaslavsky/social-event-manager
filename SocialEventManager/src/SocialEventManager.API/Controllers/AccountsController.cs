using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.BLL.Models.Users;
using SocialEventManager.DAL.Enums;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.API.Controllers
{
    using Microsoft.AspNetCore.Identity;

    [ApiController]
    [Route(ApiPathConstants.ApiController)]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Register(RegisterUserDto user)
        {
            ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(user);
            IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);

            if (!result.Succeeded)
            {
                string errorMessage = result.Errors.ToErrorMessage();
                Log.Information(errorMessage);

                return BadRequest(errorMessage);
            }

            ApplicationUser currentUser = await _userManager.FindByNameAsync(user.UserName);

            string role = RoleType.User.GetDescription();
            await _userManager.AddToRoleAsync(currentUser, role);

            await _userManager.AddClaimsAsync(applicationUser, new List<Claim>
            {
                new Claim(ClaimTypes.Sid, currentUser.Id),
                new Claim(ClaimTypes.Email, currentUser.Email),
                new Claim(ClaimTypes.Role, role),
            });

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
