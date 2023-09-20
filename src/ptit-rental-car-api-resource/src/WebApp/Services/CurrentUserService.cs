using System.Security.Claims;
using NGOT.Common.Interfaces;

namespace NGOT.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string? UserId => _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
}