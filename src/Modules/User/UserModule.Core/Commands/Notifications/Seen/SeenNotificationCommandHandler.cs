using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Notifications.Seen;

class SeenNotificationCommandHandler : IBaseCommandHandler<SeenNotificationCommand>
{
    private readonly UserContext _userContext;

    public SeenNotificationCommandHandler(UserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<OperationResult> Handle(SeenNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _userContext.UserNotifications
            .SingleOrDefaultAsync(n => n.Id == request.NotificationId &&
                                  n.UserId == request.UserId
                                  && n.IsSeen == false , cancellationToken);
        if (notification == null)
            return OperationResult.NotFound();

        notification.IsSeen = true;
        _userContext.Update(notification);
        await _userContext.SaveChangesAsync(cancellationToken);

        return OperationResult.Success();
    }
}
