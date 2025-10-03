using Common.Infrastructure.Repository;
using CoreModule.Domain.Categories.Models;
using CoreModule.Domain.Categories.Repository;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistant.Category;

class CourseCategoryRepository : BaseRepository<CourseCategory, CoreModuleEfContext>, ICourseCategoryRepository
{
    public CourseCategoryRepository(CoreModuleEfContext context) : base(context)
    {
    }

    public async Task Delete(CourseCategory courseCategory)
    {
        var categoryHasCourse = await Context.Courses
            .AnyAsync(c => c.CategoryId == courseCategory.Id || c.SubCategoryId == courseCategory.Id);

        var children = await Context.Categories.Where(c => c.ParentId == courseCategory.Id).ToListAsync();
        if (categoryHasCourse)
        {
            throw new Exception("این دسته بندی دارای دوره است");
        }

        if (children.Any())
        {
            foreach (var item in children)
            {
                var hasCourse = await Context.Courses
                    .AnyAsync(c => c.CategoryId == item.Id || c.SubCategoryId == item.Id);
                if (hasCourse)
                {
                    throw new Exception("این دسته بندی دارای دوره است");
                }
                else
                {
                    Context.Remove(item);
                }
            }
        }

        Context.Remove(courseCategory);

        await Context.SaveChangesAsync();
    }
}
