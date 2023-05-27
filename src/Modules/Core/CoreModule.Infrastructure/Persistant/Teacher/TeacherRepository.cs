using Common.Infrastructure.Repository;
using CoreModule.Domain.Teacher.Repositories;

namespace CoreModule.Infrastructure.Persistant.Teacher;

class TeacherRepository : BaseRepository<Domain.Teacher.Models.Teacher, CoreModuleEfContext>, ITeacherRepository
{
    public TeacherRepository(CoreModuleEfContext context) : base(context)
    {
    }

    public void Delete(Domain.Teacher.Models.Teacher teacher)
    {
        Context.Remove(teacher);
    }
}
