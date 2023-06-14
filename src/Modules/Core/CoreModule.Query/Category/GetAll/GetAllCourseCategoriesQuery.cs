using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Category._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Category.GetAll;

public class GetAllCourseCategoriesQuery : IQuery<List<CourseCategoryDto>>
{
}
class GetAllCourseCategoriesQueryHandler : IQueryHandler<GetAllCourseCategoriesQuery, List<CourseCategoryDto>>
{
    private readonly QueryContext _queryContext;

    public GetAllCourseCategoriesQueryHandler(QueryContext queryContext)
    {
        _queryContext = queryContext;
    }

    public async Task<List<CourseCategoryDto>> Handle(GetAllCourseCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _queryContext.Categories
           .Where(r => r.ParentId == null)
           .OrderByDescending(r => r.CreationDate)
           .Select(r => new CourseCategoryDto()
           {
               Id = r.Id,
               CreationDate = r.CreationDate,
               ParentId = r.ParentId,
               Title = r.Title,
               Slug = r.Slug

           }).ToListAsync(cancellationToken);
    }
}
