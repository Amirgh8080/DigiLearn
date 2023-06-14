using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Category._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Category.GetChildren;

public record GetCourseCategoryChildrenQuery(Guid ParentId):IQuery<List<CourseCategoryDto>>;

class GetCourseCategoryChildrenQueryHandler : IQueryHandler<GetCourseCategoryChildrenQuery, List<CourseCategoryDto>>
{
    private readonly QueryContext _queryContext;

    public GetCourseCategoryChildrenQueryHandler(QueryContext queryContext)
    {
        _queryContext = queryContext;
    }

    public async Task<List<CourseCategoryDto>> Handle(GetCourseCategoryChildrenQuery request, CancellationToken cancellationToken)
    {
        return await _queryContext.Categories
            .Where(r => r.ParentId == request.ParentId)
            .OrderByDescending(r => r.CreationDate)
            .Select( r=> new CourseCategoryDto()
            {
                Id = r.Id,
                CreationDate = r.CreationDate,
                ParentId = r.ParentId,
                Title = r.Title,
                Slug = r.Slug

            }).ToListAsync(cancellationToken);
    }
}