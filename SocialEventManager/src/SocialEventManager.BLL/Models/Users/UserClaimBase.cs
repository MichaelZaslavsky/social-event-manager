using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.BLL.Models.Users
{
    public abstract class UserClaimBase
    {
        public int Id { get; set; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string Type { get; set; }

        [Required]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.LengthMax)]
        public string Value { get; set; }
    }
}
