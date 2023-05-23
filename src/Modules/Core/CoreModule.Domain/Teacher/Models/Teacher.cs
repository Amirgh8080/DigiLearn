using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using CoreModule.Domain.Teacher.DomainServices;
using CoreModule.Domain.Teacher.Enunms;

namespace CoreModule.Domain.Teacher.Models;

public class Teacher : AggregateRoot
{
    public Teacher(Guid userId, string userName, string cvFileName, ITeacherDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(userName, nameof(userName));
        NullOrEmptyDomainDataException.CheckString(cvFileName, nameof(cvFileName));

        if (userName.IsUniCode())
            throw new InvalidDomainDataException("UserName is Invalid");

        if (domainService.DoesUserNameExists(userName))
            throw new InvalidDomainDataException("UserName Already Exists");

        UserId = userId;
        UserName = userName;
        CvFileName = cvFileName;
        Status = TeacherStatus.Pending;
    }

    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string CvFileName { get; private set; }
    public TeacherStatus Status { get; private set; }


    public void AcceptRequest()
    {
        //Event
        if (Status == TeacherStatus.Pending)
            Status = TeacherStatus.Active;
    }
    public void ToggleStatus()
    {
        if (Status == TeacherStatus.Active)
        {
            Status = TeacherStatus.DeActive;
        }
        else if (Status == TeacherStatus.DeActive)
        {
            Status = TeacherStatus.Active;
        }
    }
}
