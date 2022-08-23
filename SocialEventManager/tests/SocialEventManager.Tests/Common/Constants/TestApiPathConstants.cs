using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Tests.Common.Constants;

internal static class TestApiPathConstants
{
    public const string AccountsRegister = $"{ApiPathConstants.Accounts}/register";
    public const string AccountsLogin = $"{ApiPathConstants.Accounts}/login";
    public const string AccountsForgotPassword = $"{ApiPathConstants.Accounts}/forgot-password";
    public const string AccountsResetPassword = $"{ApiPathConstants.Accounts}/{ApiPathConstants.ResetPassword}";
}
