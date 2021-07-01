using System;
using Dapper.Contrib.Extensions;
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities
{
    [Table(TableNameConstants.Roles)]
    [CompositeIndex(true, nameof(Name))]
    public class Role
    {
        [ExplicitKey]
        public Guid Id { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
