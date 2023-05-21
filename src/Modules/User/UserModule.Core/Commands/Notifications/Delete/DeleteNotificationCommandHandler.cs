using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Notifications.Delete;

public class DeleteNotificationCommandHandler : IBaseCommandHandler<DeleteNotificationCommand>
{
    private readonly UserContext _userContext;

    public DeleteNotificationCommandHandler(UserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<OperationResult> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _userContext.UserNotifications
            .SingleOrDefaultAsync(n => n.Id == request.NotificationId &&
            n.UserId == request.UserId, cancellationToken);

        if (notification == null)
            return OperationResult.NotFound();

        _userContext.UserNotifications.Remove(notification);
        await _userContext.SaveChangesAsync(cancellationToken);

        return OperationResult.Success();
    }
}
