using Common.Application;

namespace UserModule.Core.Commands.Notifications.Seen;

public record SeenNotificationCommand(Guid UserId,Guid NotificationId):IBaseCommand;
