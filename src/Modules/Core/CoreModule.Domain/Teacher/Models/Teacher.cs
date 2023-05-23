using Common.Domain;
using CoreModule.Domain.Teacher.Enunms;

namespace CoreModule.Domain.Teacher.Models;

public class Teacher:AggregateRoot
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string CvFileName{ get; set; }
    public TeacherStatus Status { get; set; }
}
