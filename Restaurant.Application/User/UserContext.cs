using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurant.Application.User;

public class UserContext(IHttpContextAccessor httpContext) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContext.HttpContext?.User;
        if (user == null) throw new InvalidOperationException("User Context is not present");

        if (user.Identity is null || !user.Identity.IsAuthenticated)
        {
            return null;
        }
        var userId = user.FindFirst(tmp => tmp.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(tmp => tmp.Type == ClaimTypes.Email)!.Value;
        var role = user.Claims.Where(tmp => tmp.Type == ClaimTypes.Role)!.Select(tmp => tmp.Value);
        return new CurrentUser(Id: userId, Email: email, role);
    }
}
