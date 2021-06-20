using System;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities
{
    [Table(TableNameConstants.Roles)]
    public class Role
    {
        [ExplicitKey]
        public Guid Id { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
