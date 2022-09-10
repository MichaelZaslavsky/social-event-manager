using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Contact;

public record ContactDto
{
    public ContactDto(string name, string email, string text)
    {
        Name = name;
        Email = email;
        Text = text;
    }

    [Required]
    [StringLength(LengthConstants.Length255, MinimumLength = LengthConstants.Length2)]
    public string Name { get; init; }

    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    [StringLength(LengthConstants.LengthMax, MinimumLength = LengthConstants.Length2)]
    public string Text { get; init; }
}
