using Common.Domain;
using CoreModule.Domain.Teacher.Enunms;

namespace CoreModule.Domain.Teacher.Models;

public class Teacher : AggregateRoot
{
    public Teacher(Guid userId, string userName, string cvFileName, TeacherStatus status)
    {
        UserId = userId;
        UserName = userName;
        CvFileName = cvFileName;
        Status = status;
    }

    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string CvFileName { get; private set; }
    public TeacherStatus Status { get; private set; }
}
