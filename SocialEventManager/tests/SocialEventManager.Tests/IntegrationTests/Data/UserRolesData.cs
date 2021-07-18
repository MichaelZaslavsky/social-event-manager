using System;
using System.Collections.Generic;
using System.Linq;
using SocialEventManager.DAL.Entities;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.IntegrationTests.Data
{
    public sealed class UserRolesData
    {
        private UserRolesData()
        {
            UserRoles.AddRange(RolesData.Instance.Roles.Select(r => new UserRole
            {
                Id = RandomGeneratorHelpers.NextInt32(),
                UserId = Guid.NewGuid(),
                RoleId = r.Id,
            }));
        }

        private static readonly Lazy<UserRolesData> Lazy = new(() => new UserRolesData());

        public static UserRolesData Instance
        {
            get
            {
                return Lazy.Value;
            }
        }

        public List<UserRole> UserRoles { get; set; }
    }
}
