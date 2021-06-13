using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SocialEventManager.BLL.Models.Identity;
using SocialEventManager.BLL.Services;

namespace SocialEventManager.API.DependencyInjection
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityConfigurations(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationUserRole>()
                .AddUserStore<CustomUserStoreService>()
                .AddRoleStore<CustomRoleStoreService>()
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
