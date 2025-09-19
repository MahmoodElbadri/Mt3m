using MediatR;

namespace Restaurant.Application.Users.Commands;

public class UpdateUserCommand : IRequest
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}
