namespace CoreModule.Domain.Teacher.DomainServices;

public interface ITeacherDomainService
{
    bool DoesUserNameExists(string userName);
}
