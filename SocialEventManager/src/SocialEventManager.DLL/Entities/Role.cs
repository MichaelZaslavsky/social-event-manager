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
        [Required]
        [ExplicitKey]
        [PrimaryKey]
        public Guid Id { get; set; }

        [Required]
        [StringLength(LengthConstants.Length255)]
        public string ConcurrencyStamp { get; set; }

        [Required]
        [StringLength(LengthConstants.Length255)]
        [Unique]
        public string Name { get; set; }

        [Required]
        [StringLength(LengthConstants.Length255)]
        public string NormalizedName { get; set; }
    }
}
