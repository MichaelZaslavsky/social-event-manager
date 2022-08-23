using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Models.Auth;

namespace SocialEventManager.Infrastructure.Identity;

using SocialEventManager.Shared.Constants;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        RemoveAspNetPrefixFromIdentityTables(builder);
        SeedRoles(builder);
    }

    private static void RemoveAspNetPrefixFromIdentityTables(ModelBuilder builder)
    {
        foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            string? tableName = entityType.GetTableName();

            if (tableName is not null && tableName.StartsWith(IdentityConstants.AspNet))
            {
                entityType.SetTableName(tableName.TakeAfterFirst(IdentityConstants.AspNet));
            }
        }
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid>()
            {
                Id = Guid.NewGuid(),
                Name = Shared.Constants.UserRoles.User,
                NormalizedName = Shared.Constants.UserRoles.User.ToUpper(),
            });
    }
}
