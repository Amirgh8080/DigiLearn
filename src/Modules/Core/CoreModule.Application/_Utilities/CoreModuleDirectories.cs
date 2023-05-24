namespace CoreModule.Application._Utilities;

public class CoreModuleDirectories
{
    public static string CvFileNames = "wwwroot/core/teacher";

    public static string CourseImages = "wwwroot/core/course";

    public static string CourseDemo(Guid CourseId) => $"course/{CourseId}";
}
