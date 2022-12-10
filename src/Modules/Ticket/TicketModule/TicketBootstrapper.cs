using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using TicketModule.Core.Services;
using TicketModule.Data;

namespace TicketModule;

public static class TicketBootstrapper
{
    public static IServiceCollection InitTicketModule(this IServiceCollection services, IConfiguration configuraion)
    {
        services.AddDbContext<TicketContext>(option =>
        {
            option.UseSqlServer(configuraion.GetConnectionString("Ticket_Context"));
        });

        services.AddAutoMapper(typeof(MapperProfile).Assembly);
        services.AddScoped<ITicketService, TicketService>();

        return services;
    }
}
