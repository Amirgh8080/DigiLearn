using Common.Domain.Repository;
using CoreModule.Domain.Teacher;

namespace CoreModule.Domain.Teacher.Repositories;

public interface ITeacherRepository:IBaseRepository<Models.Teacher>
{
    void Delete(Models.Teacher teacher);
}
