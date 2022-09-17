namespace SocialEventManager.Shared.Constants;

public static class GlobalConstants
{
    public const string MachineName = nameof(MachineName);
    public const string EntryPoint = nameof(EntryPoint);
    public const string Service = nameof(Service);
    public const string Repository = nameof(Repository);
    public const string Fake = nameof(Fake);
    public const string Stub = nameof(Stub);
    public const string InterfacePrefix = "I";
    public const string Localhost = "localhost";
    public const string Cascade = "CASCADE";
    public const int Size = 1000;

    public const string DapperIdentityObsoleteReason = @"
        This is an example of a partial Identity implementation with Dapper.
        It was just for learning purposes.
        It is much more recommended to use the Identity packages with EF.";
}
