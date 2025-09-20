using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Restaurant.Application.Authorization;

public class RestaurantUserClaimsPrincipalFactory(
    UserManager<Domain.Entities.User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
    : UserClaimsPrincipalFactory<Domain.Entities.User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(Domain.Entities.User user)
    {
        var id = await GenerateClaimsAsync(user);
        if (user.Nationality is not null)
        {
            id.AddClaim(new Claim("Nationality", user.Nationality));
        }
        if ((user.DateOfBirth is not null))
        {
            id.AddClaim(new Claim("DateOfBirth", user.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty));
        }
        return new ClaimsPrincipal(id);
    }
}
