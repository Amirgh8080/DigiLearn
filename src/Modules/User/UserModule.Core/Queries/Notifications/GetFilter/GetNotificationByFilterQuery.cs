using Common.Query;
using UserModule.Core.Queries._DTOs;

namespace UserModule.Core.Queries.Notifications.GetFilter;

public class GetNotificationByFilterQuery : QueryFilter<NotificationFilterResult, NotificationFilterParams>
{
    public GetNotificationByFilterQuery(NotificationFilterParams filterParams) : base(filterParams)
    {
    }
}
