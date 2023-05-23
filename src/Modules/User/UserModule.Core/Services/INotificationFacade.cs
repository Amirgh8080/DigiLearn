using Common.Application;
using MediatR;
using UserModule.Core.Commands.Notifications.Create;
using UserModule.Core.Commands.Notifications.Delete;
using UserModule.Core.Commands.Notifications.DeleteAll;
using UserModule.Core.Commands.Notifications.Seen;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Queries.Notifications.GetFilter;

namespace UserModule.Core.Services;

public interface INotificationFacade
{
    Task<OperationResult> Create(CreateNotificationCommand command);
    Task<OperationResult> Delete(DeleteNotificationCommand command);
    Task<OperationResult> DeleteAll(DeleteAllNotificationCommand command);
    Task<OperationResult> Seen(SeenNotificationCommand command);


    Task<NotificationFilterResult> GetNotifications(NotificationFilterParams filterParams);
}
class NotificationFacade : INotificationFacade
{
    private readonly IMediator _mediator;

    public NotificationFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateNotificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(DeleteNotificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteAll(DeleteAllNotificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<NotificationFilterResult> GetNotifications(NotificationFilterParams filterParams)
    {
        return await _mediator.Send(new GetNotificationByFilterQuery(filterParams));
    }

    public async Task<OperationResult> Seen(SeenNotificationCommand command)
    {
        return await _mediator.Send(command);
    }
}