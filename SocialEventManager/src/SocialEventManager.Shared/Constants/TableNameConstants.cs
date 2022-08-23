namespace SocialEventManager.Shared.Constants;

public static class TableNameConstants
{
    public const string Accounts = $"{SchemaConstants.Default}.{nameof(Accounts)}";
    public const string Changelog = nameof(Changelog);
    public const string EntityFrameworkHistory = "EFHistory";
    public const string Roles = $"{SchemaConstants.Default}.{nameof(Roles)}";
    public const string SchemaTypes = nameof(SchemaTypes);
    public const string UserClaims = $"{SchemaConstants.Default}.{nameof(UserClaims)}";
    public const string UserRoles = $"{SchemaConstants.Default}.{nameof(UserRoles)}";
}
