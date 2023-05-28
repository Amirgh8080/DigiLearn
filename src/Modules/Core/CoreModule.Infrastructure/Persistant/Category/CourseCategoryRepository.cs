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
            .AnyAsync(c => c.CategoryId == courseCategory.Id|| c.SubCategoryId == courseCategory.Id);
        
        if (categoryHasCourse)
        {
            throw new Exception("این دسته بندی دارای دوره است");
        }

        //TODO : Remove Child

         Context.Remove(courseCategory);

        await Context.SaveChangesAsync();
    }
}
