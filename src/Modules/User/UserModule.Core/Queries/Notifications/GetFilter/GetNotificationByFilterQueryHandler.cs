using Common.Query;
using Microsoft.EntityFrameworkCore;
using UserModule.Core.Queries._DTOs;
using UserModule.Data;

namespace UserModule.Core.Queries.Notifications.GetFilter;

public class GetNotificationByFilterQueryHandler : IQueryHandler<GetNotificationByFilterQuery, NotificationFilterResult>
{
    private readonly UserContext _userContext;

    public GetNotificationByFilterQueryHandler(UserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<NotificationFilterResult> Handle(GetNotificationByFilterQuery request, CancellationToken cancellationToken)
    {
        var result =  _userContext.UserNotifications.Where(n => n.UserId == request.FilterParams.UserId)
            .AsQueryable();

        if (request.FilterParams.IsSeen != null)
            result = result.Where(n => n.IsSeen == request.FilterParams.IsSeen);

        var skip = (request.FilterParams.PageId - 1) * request.FilterParams.Take;

        var model = new NotificationFilterResult()
        {
            Data =await result.Skip(skip).Take(request.FilterParams.Take)
            .Select(s=> new NotificationFilterData()
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                IsSeen = s.IsSeen,
                Text = s.Text,
                Title = s.Title,
                UserId = s.UserId
            }).ToListAsync(cancellationToken)
        };

        model.GeneratePaging(result, request.FilterParams.Take, request.FilterParams.PageId);

        return model;
    }
}
