using CoreModule.Domain.Categories.Repository;
using CoreModule.Domain.Course.Repository;
using CoreModule.Domain.Teacher.Repositories;
using CoreModule.Infrastructure.Persistant;
using CoreModule.Infrastructure.Persistant.Category;
using CoreModule.Infrastructure.Persistant.Course;
using CoreModule.Infrastructure.Persistant.Teacher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Infrastructure;

public class CoreModuleInfrastructureBootstrapper
{
    public static void RegisterDependency(IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();

        services.AddDbContext<CoreModuleEfContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("CoreModule_Context"));
        });
    }
}
