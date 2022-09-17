namespace SocialEventManager.Shared.Models.Auth;

public sealed record UserRegistrationDto(string FirstName, string LastName, string Email, string Password, string ConfirmPassword);
