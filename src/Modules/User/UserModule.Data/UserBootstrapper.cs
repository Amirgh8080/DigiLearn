using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace UserModule.Data;

public static class UserBootstrapper
{
    public static IServiceCollection InitUserModule(this IServiceCollection services, IConfiguration configuraion)
    {
        services.AddDbContext<UserContext>(option =>
        {
            option.UseSqlServer(configuraion.GetConnectionString("User_Context"));
        });


        return services;
    }
}
