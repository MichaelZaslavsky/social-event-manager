namespace SocialEventManager.Shared.Models.Auth;

public sealed record ResetPasswordDto(string Email, string Token, string NewPassword, string ConfirmPassword);
