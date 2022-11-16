using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace BlogModule;

public static class BlogBootstrapper
{
    public static IServiceCollection InitBlogModule(this IServiceCollection services)
    {
        return services;
    }
}
