using Dapper.Contrib.Extensions;
using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities
{
    [Table(TableNameConstants.Roles)]
    [Alias(AliasConstants.Roles)]
    [UniqueConstraint(nameof(Name))]
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
        public string Name { get; set; }

        [Required]
        [StringLength(LengthConstants.Length255)]
        public string NormalizedName { get; set; }
    }
}
