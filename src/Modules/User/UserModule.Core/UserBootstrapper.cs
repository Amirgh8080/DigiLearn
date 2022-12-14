using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.Core.Services;
using UserModule.Data;

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

        services.AddValidatorsFromAssembly(typeof(UserBootstrapper).Assembly);
        services.AddMediatR(typeof(UserBootstrapper).Assembly);
        services.AddAutoMapper(typeof(MapperProfile).Assembly);

        return services;
    }
}
