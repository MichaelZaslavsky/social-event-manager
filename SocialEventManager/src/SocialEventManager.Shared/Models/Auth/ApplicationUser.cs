using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Auth;

public class ApplicationUser : IdentityUser<Guid>
{
    [Required]
    [StringLength(LengthConstants.Length255)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(LengthConstants.Length255)]
    public string LastName { get; set; } = null!;
}
