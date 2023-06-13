using CoreModule.Domain.Teacher.DomainServices;
using CoreModule.Domain.Teacher.Repositories;

namespace CoreModule.Application.Teacheres;

public class TeacherDomainServices : ITeacherDomainService
{
    private readonly ITeacherRepository _repository;

    public TeacherDomainServices(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public bool DoesUserNameExists(string userName)
    {
        return  _repository.Exists(f=>f.UserName == userName.ToLower());
    }
}
