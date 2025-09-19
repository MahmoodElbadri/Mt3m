using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Application.User;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserCommandHandler (IUserContext userContext, ILogger<UpdateUserCommandHandler> logger
    ,IUserStore<Domain.Entities.User> userStore)
    : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation($"Updating User {user?.Id} with {request}");
        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
        if (dbUser is null)
        {
            throw new NotFoundException(nameof(Domain.Entities.User), user.Id);
        }
        dbUser.DateOfBirth = request.DateOfBirth;
        dbUser.Nationality = request.Nationality;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
