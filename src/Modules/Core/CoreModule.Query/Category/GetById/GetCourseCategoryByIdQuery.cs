using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Category._DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CoreModule.Query.Category.GetById;

public record GetCourseCategoryByIdQuery(Guid CateogryId) : IQuery<CourseCategoryDto?>;

class GetCourseCategoryByIdQueryHandler : IQueryHandler<GetCourseCategoryByIdQuery, CourseCategoryDto?>
{
    private readonly QueryContext _context;

    public GetCourseCategoryByIdQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<CourseCategoryDto?> Handle(GetCourseCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.Include(r => r.Children).FirstOrDefaultAsync(c => c.Id == request.CateogryId);
        if (category == null)
            return null;

        return new CourseCategoryDto()
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            ParentId = category.ParentId,
            Title = category.Title,
            Slug = category.Slug,
            Children = category.Children.Select(s => new CourseCategoryChildDto()
            {
                Id = s.Id,
                Title = s.Title,
                Slug = s.Slug,
                CreationDate = s.CreationDate,
                ParentId = s.ParentId
            }).ToList()

        };
    }
}
