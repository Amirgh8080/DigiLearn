using CoreModule.Application.Categories.Create;
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


            return services;
        }
    }
}