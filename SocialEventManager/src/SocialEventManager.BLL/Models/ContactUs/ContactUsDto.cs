using System.ComponentModel.DataAnnotations;

namespace SocialEventManager.BLL.Models.ContactUs;

public record ContactUsDto
{
    public ContactUsDto(string name, string email, string text)
    {
        Name = name;
        Email = email;
        Text = text;
    }

    [Required]
    [MinLength(2)]
    public string Name { get; init; }

    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    [MinLength(10)]
    public string Text { get; init; }
}
