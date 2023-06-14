namespace CoreModule.Application._Utilities;

public class CoreModuleDirectories
{
    public static string CvFileNames = "wwwroot/core/teacher";

    public static string CourseImages = "wwwroot/core/course";

    public static string CourseDemo(Guid courseId) => $"course/{courseId}";
    public static string GetCourseImage(string courseImage) => $"{CourseImages.Replace("wwww","")}/{courseImage}";
}
