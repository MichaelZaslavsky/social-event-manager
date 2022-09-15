namespace SocialEventManager.Shared.Models.Auth;

public record ResetPasswordDto(string Email, string Token, string NewPassword, string ConfirmPassword);
