using Common.Infrastructure.Repository;
using CoreModule.Domain.Course.Repository;

namespace CoreModule.Infrastructure.Persistant.Course;

class CourseRepository : BaseRepository<Domain.Course.Models.Course, CoreModuleEfContext>, ICourseRepository
{
    public CourseRepository(CoreModuleEfContext context) : base(context)
    {
    }
}
