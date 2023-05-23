using Common.Application;
using Microsoft.AspNetCore.Http;

public class RegisterTeacherCommand : IBaseCommand
{
    public IFormFile CvFile { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
}
