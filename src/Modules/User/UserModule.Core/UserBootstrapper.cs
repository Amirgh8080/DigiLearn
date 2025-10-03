using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.Core.Services;
using UserModule.Data;
using AutoMapper;


namespace UserModule.Core;

public static class UserBootstrapper
{
    public static IServiceCollection InitUserModule(this IServiceCollection services, IConfiguration configuraion)
    {
        services.AddDbContext<UserContext>(option =>
        {
            option.UseSqlServer(configuraion.GetConnectionString("User_Context"));
        });

        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<INotificationFacade, NotificationFacade>();

        services.AddValidatorsFromAssembly(typeof(UserBootstrapper).Assembly);
        services.AddMediatR(typeof(UserBootstrapper).Assembly);
        services.AddAutoMapper(typeof(UserBootstrapper).Assembly);
        
        return services;
    }
}
