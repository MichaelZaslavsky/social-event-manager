using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Tests.Common.Constants;

internal static class TestApiPathConstants
{
    public const string AccountsConfirmEmail = $"{ApiPathConstants.Accounts}/{ConfirmEmail}";
    public const string AccountsForgotPassword = $"{ApiPathConstants.Accounts}/forgot-password";
    public const string AccountsLogin = $"{ApiPathConstants.Accounts}/login";
    public const string AccountsRegister = $"{ApiPathConstants.Accounts}/register";
    public const string AccountsResetPassword = $"{ApiPathConstants.Accounts}/{ApiPathConstants.ResetPassword}";
    public const string ConfirmEmail = "confirm-email";
}
