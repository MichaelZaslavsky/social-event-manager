using System;
using Dapper.Contrib.Extensions;
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities
{
    [Table(TableNameConstants.Roles)]
    [Alias(AliasConstants.Roles)]
    public class Role
    {
        [ExplicitKey]
        [PrimaryKey]
        public Guid Id { get; set; }

        public string ConcurrencyStamp { get; set; }

        [Unique]
        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
