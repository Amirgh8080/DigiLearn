using Common.Infrastructure;
using CoreModule.Domain.Categories.Models;
using CoreModule.Infrastructure.Persistant.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistant;

public class CoreModuleEfContext : BaseEfContext<CoreModuleEfContext>
{
    public CoreModuleEfContext(DbContextOptions<CoreModuleEfContext> options, IMediator mediator) : base(options, mediator)
    {
    }

    public DbSet<Domain.Course.Models.Course> Courses { get; set; }
    public DbSet<Domain.Teacher.Models.Teacher> Teachers { get; set; }
    public DbSet<CourseCategory> Categories { get; set; }
    private DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        base.OnModelCreating(modelBuilder);
    }
}
