using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialEventManager.Shared.Configurations;

namespace SocialEventManager.Infrastructure.Auth;

public sealed class JwtHandler : IJwtHandler
{
    private readonly JwtConfiguration _jwtConfiguration;

    public JwtHandler(IOptions<JwtConfiguration> jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public string GenerateToken(string email)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256Signature);
        DateTime expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_jwtConfiguration.ExpiryInDays));

        JwtSecurityToken token = new(
            _jwtConfiguration.Issuer,
            _jwtConfiguration.Audience,
            new Claim[] { new(JwtRegisteredClaimNames.Email, email) },
            DateTime.UtcNow,
            expires,
            creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public IDictionary<string, string> GetTokenInfo(string token)
    {
        Dictionary<string, string> tokenInfo = new();

        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(token);

        foreach (Claim claim in jwtSecurityToken.Claims)
        {
            tokenInfo.Add(claim.Type, claim.Value);
        }

        return tokenInfo;
    }
}
