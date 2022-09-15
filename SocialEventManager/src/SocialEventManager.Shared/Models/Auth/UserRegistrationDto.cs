namespace SocialEventManager.Shared.Models.Auth;

public record UserRegistrationDto(string FirstName, string LastName, string Email, string Password, string ConfirmPassword);
