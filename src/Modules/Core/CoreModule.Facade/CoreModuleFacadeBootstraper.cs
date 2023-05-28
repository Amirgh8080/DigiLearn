using CoreModule.Facade.Category;
using CoreModule.Facade.Course;
using CoreModule.Facade.Teacher;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Facade;

public class CoreModuleFacadeBootstraper
{
    public static void RegisterDependency(IServiceCollection services)
    {
        services.AddScoped<ITeacherFacade,TeacherFacade>();
        services.AddScoped<ICourseCategoryFacade,CourseCategoryFacade>();
        services.AddScoped<ICourseFacade,CourseFacade>();
    }
}
