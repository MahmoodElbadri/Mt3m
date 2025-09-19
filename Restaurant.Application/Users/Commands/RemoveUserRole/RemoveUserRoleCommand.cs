using MediatR;

namespace Restaurant.Application.Users.Commands.RemoveUserRole;

public class RemoveUserRoleCommand:IRequest
{
    public string? UserEmail { get; set; }
    public string? RoleName { get; set; }
}
