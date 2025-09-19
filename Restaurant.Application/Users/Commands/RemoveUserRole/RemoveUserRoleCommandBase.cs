using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.Users.Commands.RemoveUserRole;

public class RemoveUserRoleCommandHandler(ILogger<RemoveUserRoleCommandHandler> logger,
    UserManager<Domain.Entities.User> userManager,
    RoleManager<IdentityRole> roleManager
    ) : IRequestHandler<RemoveUserRoleCommand>
{
    public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Remove UserRole invoked with UserEmail: {UserEmail}, UserRole: {RoleName}", request.UserEmail, request.RoleName);
        var user = await userManager.FindByEmailAsync(request.UserEmail!) ??
            throw new NotFoundException(nameof(IdentityUser), request.UserEmail!);

        var role = await roleManager.FindByNameAsync(request.RoleName!) ??
            throw new NotFoundException(nameof(IdentityRole), request.RoleName!);

        var userRoles = await userManager.GetRolesAsync(user);
        if (!userRoles.Contains(request.RoleName!))
        {
            throw new NotFoundException(nameof(IdentityRole), request.RoleName!);
        }
        var result = await userManager.RemoveFromRoleAsync(user, request.RoleName!);
    }
}