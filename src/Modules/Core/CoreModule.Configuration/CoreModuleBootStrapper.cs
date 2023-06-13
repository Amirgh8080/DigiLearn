using CoreModule.Application.Categories;
using CoreModule.Application.Categories.Create;
using CoreModule.Application.Courses;
using CoreModule.Application.Teacheres;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Course.DomainServices;
using CoreModule.Domain.Teacher.DomainServices;
using CoreModule.Facade;
using CoreModule.Infrastructure;
using CoreModule.Query;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Configuration
{
    public static class CoreModuleBootStrapper
    {
        public static IServiceCollection InitCoreModule(this IServiceCollection services,IConfiguration configuration)
        {
            CoreModuleFacadeBootstraper.RegisterDependency(services);
            CoreModuleInfrastructureBootstrapper.RegisterDependency(services,configuration);
            CoreModuleQueryBootstrapper.RegisterDependency(services, configuration);

            services.AddMediatR(typeof(CreateCategoryCommand).Assembly);
            services.AddValidatorsFromAssembly(typeof(CreateCategoryCommand).Assembly);


            services.AddScoped<ICourseDomainService,CourseDomainService>();
            services.AddScoped<ICategoryDomainService,CategoryDomainService>();
            services.AddScoped<ITeacherDomainService,TeacherDomainServices>();

            return services;
        }
    }
}