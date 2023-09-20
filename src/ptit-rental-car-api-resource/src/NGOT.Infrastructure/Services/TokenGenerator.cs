using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NGOT.Common.Interfaces;
using NGOT.Common.Settings;

namespace NGOT.Infrastructure.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IOptions<JwtSettings> _jwtOptions;

    public TokenGenerator(IOptions<JwtSettings> jwtOptions, IDateTimeProvider dateTimeProvider)
    {
        _jwtOptions = jwtOptions;
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateAccessToken(ClaimsIdentity subject, Dictionary<string, object?> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtOptions.Value.Secret);
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Audience = _jwtOptions.Value.Audience,
            Claims = claims,
            Issuer = _jwtOptions.Value.Issuer,
            IssuedAt = _dateTimeProvider.Now,
            Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtOptions.Value.AccessTokenExpiration)),
            SigningCredentials = signingCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}