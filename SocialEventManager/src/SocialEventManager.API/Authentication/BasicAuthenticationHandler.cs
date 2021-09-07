using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SocialEventManager.BLL.Models.Users;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Constants.Validations;

namespace SocialEventManager.API.Authentication
{
    // Temporary implementation of basic authentication until there is an authentication in the application.
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(AuthConstants.Authorization))
            {
                return Task.FromResult(AuthenticateResult.Fail(AuthValidationConstants.MissingAuthorizationHeader));
            }

            try
            {
                LoginModel login = GetLoginCredentials();

                if (login.UserName == "TempUser" && login.Password == "TempPassword")
                {
                    Claim[] claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, login.UserName),
                    };

                    ClaimsIdentity identity = new(claims, Scheme.Name);
                    ClaimsPrincipal principal = new(identity);
                    AuthenticationTicket ticket = new(principal, Scheme.Name);

                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }

                return Task.FromResult(AuthenticateResult.Fail(AuthValidationConstants.InvalidUserNameOrPassword));
            }
            catch
            {
                return Task.FromResult(AuthenticateResult.Fail(AuthValidationConstants.InvalidAuthorizationHeader));
            }
        }

        private LoginModel GetLoginCredentials()
        {
            AuthenticationHeaderValue authenticationHeader = AuthenticationHeaderValue.Parse(Request.Headers[AuthConstants.Authorization]);

            byte[] credentialBytes = Convert.FromBase64String(authenticationHeader.Parameter);
            string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            string username = credentials[0];
            string password = credentials[1];

            return new LoginModel
            {
                UserName = username,
                Password = password,
            };
        }
    }
}
