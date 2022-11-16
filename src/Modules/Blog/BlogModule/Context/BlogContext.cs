using BlogModule.Domain;
using Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogModule.Context;

internal class BlogContext : BaseEfContext<DbContext>
{
    public BlogContext(DbContextOptions<DbContext> options, IMediator mediator) : base(options, mediator)
    {
    }


    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }  
}
