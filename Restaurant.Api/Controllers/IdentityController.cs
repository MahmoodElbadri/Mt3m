using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Users.Commands.AssignUserRole;
using Restaurant.Application.Users.Commands.UpdateUserDetails;
using Restaurant.Domain.Constants;

namespace Restaurant.Api.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class IdentityController (IMediator _mediator): ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserCommand updateUserCommand)
    {
        await _mediator.Send(updateUserCommand);
        return NoContent();
    }

    [HttpPatch("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
