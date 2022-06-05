using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.BLL.Services.Identity;

namespace SocialEventManager.API.DependencyInjection
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityConfigurations(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddUserStore<CustomUsersStore>()
                .AddRoleStore<CustomRolesStore>()
                .AddErrorDescriber<IdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            services
                .Configure((Action<IdentityOptions>)(options =>
                {
                    SetPasswordSettings(options.Password);
                    SetLockSettings(options.Lockout);
                    SetUserSettings(options.User);
                }))
                .ConfigureApplicationCookie(options =>
                {
                    SetCookieSettings(options.Cookie);
                    SetPathSettings(options);

                    options.ExpireTimeSpan = TimeSpan.FromHours(12);
                    options.SlidingExpiration = true;
                });

            return services;
        }

        #region Private Methods

        private static void SetPasswordSettings(PasswordOptions password)
        {
            password.RequireDigit = false;
            password.RequiredLength = 6;
            password.RequireNonAlphanumeric = false;
            password.RequireUppercase = false;
            password.RequireLowercase = false;
        }

        private static void SetLockSettings(LockoutOptions lockout)
        {
            lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            lockout.MaxFailedAccessAttempts = 3;
        }

        private static void SetUserSettings(UserOptions user)
        {
            user.RequireUniqueEmail = true;
        }

        private static void SetCookieSettings(CookieBuilder cookie)
        {
            cookie.HttpOnly = true;
        }

        private static void SetPathSettings(CookieAuthenticationOptions options)
        {
            const string accountPath = "/api/Account";

            options.LoginPath = $"{accountPath}/login";
            options.LogoutPath = $"{accountPath}/logout";
            options.AccessDeniedPath = $"{accountPath}/access-denied";
        }

        #endregion Private Methods
    }
}
