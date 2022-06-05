using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.BLL.Models.Roles
{
    public abstract record RoleBase
    {
        [Required]
        [NotDefault]
        public Guid Id { get; init; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string ConcurrencyStamp { get; init; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string Name { get; init; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string NormalizedName { get; init; }
    }
}
