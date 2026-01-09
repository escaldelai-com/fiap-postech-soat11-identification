using Microsoft.IdentityModel.Tokens;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.WebApi;
using Restaurant.Identification.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Restaurant.Identification.WebApi.Services;

public class TokenCreateService(
    IConfiguration config) : ITokenCreateService
{

    private readonly byte[] key = Convert.FromBase64String(config["Security:PrivateKey"]
        ?? throw new ArgumentNullException("Security:PrivateKey"));

    private readonly TimeSpan expireTime = TimeSpan.Parse(config["Security:ExpireTime"]
        ?? throw new ArgumentNullException("Security:ExpireTime"));

    private readonly string issuer = config["Security:Issuer"]
        ?? throw new ArgumentNullException("Security:Issuer");




    public TokenDto Create(ServiceDto data)
    {
        if (string.IsNullOrEmpty(data?.Name))
            throw new NotAuthorizedException();

        var desc = GetDescriptor(data.Name!, data.Audiences, data.Roles);
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(desc);

        return new TokenDto
        {
            AccessToken = tokenHandler.WriteToken(token),
            ExpiresIn = expireTime.TotalSeconds,
            RefreshToken = null,
            TokenType = "Bearer"
        };
    }


    private SecurityTokenDescriptor GetDescriptor(string name, IEnumerable<string> audiences, IEnumerable<string> roles)
    {
        var desc = new SecurityTokenDescriptor();

        desc.Expires = DateTime.UtcNow.Add(expireTime);
        desc.Issuer = issuer;
        desc.SigningCredentials = new SigningCredentials(GetKey(), SecurityAlgorithms.RsaSha256);
        desc.Subject = new ClaimsIdentity();
        desc.Subject.AddClaim(new Claim(ClaimTypes.Name, name));
        desc.Subject.AddClaims(roles.Distinct().Select(x => new Claim(ClaimTypes.Role, x)));

        foreach (var audience in audiences)
            desc.Audiences.Add(audience);

        return desc;
    }

    private RsaSecurityKey GetKey()
    {
        var rsa = RSA.Create();

        rsa.ImportRSAPrivateKey(key, out _);

        return new RsaSecurityKey(rsa);
    }


}
