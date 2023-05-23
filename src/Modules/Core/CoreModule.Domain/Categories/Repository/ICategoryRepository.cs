using Common.Domain.Repository;
using CoreModule.Domain.Categories.Models;

namespace CoreModule.Domain.Categories.Repository;

public interface ICategoryRepository : IBaseRepository<CourseCategory>
{
    Task Delete(CourseCategory courseCategory);
}
