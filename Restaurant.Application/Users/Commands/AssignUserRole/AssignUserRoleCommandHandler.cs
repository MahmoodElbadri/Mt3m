using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger,
    UserManager<Domain.Entities.User> userManager, RoleManager<IdentityRole> roleManager
    ) : IRequestHandler<AssignUserRoleCommand>
{

    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assign UserRole invoked with UserEmail: {UserEmail}, UserRole: {RoleName}", request.UserEmail, request.RoleName);
        var user = await userManager.FindByEmailAsync(request.UserEmail!) ??
            throw new NotFoundException(nameof(Domain.Entities.User), request.UserEmail!);

        var role = await roleManager.FindByNameAsync(request.RoleName!) ??
                   throw new NotFoundException("Role", request.RoleName!);

        var result = await userManager.AddToRoleAsync(user, role.Name!);
    }
}
