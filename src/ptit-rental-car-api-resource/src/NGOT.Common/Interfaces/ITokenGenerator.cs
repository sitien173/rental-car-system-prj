using System.Security.Claims;

namespace NGOT.Common.Interfaces;

public interface ITokenGenerator : ITransient
{
    string GenerateAccessToken(ClaimsIdentity subject, Dictionary<string, object?> claims);
    string GenerateRefreshToken();
}