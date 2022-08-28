using Microsoft.AspNetCore.Identity;
using SocialEventManager.Infrastructure.Identity;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.API.DependencyInjection;

using SocialEventManager.Shared.Constants;

public static class IdentityServiceCollectionExtensions
{
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(opt =>
        {
            opt.Password.RequiredLength = IdentityConstants.MinPasswordLength;
            opt.User.RequireUniqueEmail = true;
            opt.SignIn.RequireConfirmedEmail = true;
            opt.Lockout.MaxFailedAccessAttempts = IdentityConstants.MaxFailedAccessAttempts;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
