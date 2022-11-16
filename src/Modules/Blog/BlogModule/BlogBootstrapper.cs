using BlogModule.Context;
using BlogModule.Repositories.Categories;
using BlogModule.Repositories.Posts;
using BlogModule.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace BlogModule;

public static class BlogBootstrapper
{
    public static IServiceCollection InitBlogModule(this IServiceCollection services,IConfiguration configuraion)
    {
        services.AddDbContext<BlogContext>(option =>
        {
            option.UseSqlServer(configuraion.GetConnectionString("Blog_Context"));
        });

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        
        
        services.AddScoped<IBlogService, BlogService>();


        services.AddAutoMapper(typeof(MapperProfile).Assembly);

        return services;
    }
}
