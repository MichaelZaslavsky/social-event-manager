using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.BLL.Models.Users
{
    public abstract record UserClaimBase
    {
        public int Id { get; init; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string Type { get; init; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.LengthMax)]
        public string Value { get; init; }
    }
}
