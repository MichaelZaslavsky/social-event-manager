namespace SocialEventManager.Infrastructure.Auth;

public interface IJwtHandler
{
    string GenerateToken(string email);

    IDictionary<string, string> GetTokenInfo(string token);
}
