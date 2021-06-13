using System;
using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;

namespace SocialEventManager.BLL.Models
{
    public class BaseUserRoleDto
    {
        public BaseUserRoleDto(string userId, string roleName)
        {
            UserId = Guid.Parse(userId);
            RoleName = roleName;
        }

        public BaseUserRoleDto(Guid userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }

        [Required]
        [NotDefault]
        public Guid UserId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(LengthConstants.Length2)]
        [MaxLength(LengthConstants.Length255)]
        public string RoleName { get; set; }
    }
}
