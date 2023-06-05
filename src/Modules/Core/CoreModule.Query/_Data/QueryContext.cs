using CoreModule.Query._Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query._Data;

class QueryContext : DbContext
{
    public QueryContext(DbContextOptions<QueryContext> options) : base(options)
    {

    }
    public DbSet<UserQueryModel> Users { get; set; }
    public DbSet<TeacherQueryModel> Teachers { get; set; }
    public DbSet<CourseQueryModel> Courses { get; set; }
    public DbSet<SectionQueryModel> Sections { get; set; }
    public DbSet<EpisodeQueryModel> Episodes { get; set; }
    public DbSet<CategoryQueryModel> Categories { get; set; }


    public override int SaveChanges()
    {
        throw new NotImplementedException();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        

 
        modelBuilder.Entity<TeacherQueryModel>(builder =>
        {
            builder.Property(b => b.UserName)
           .IsRequired()
           .IsUnicode(false)
           .HasMaxLength(25);

            builder.Property(b => b.CvFileName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<CourseQueryModel>(builder =>
        {
            builder.OwnsOne(p => p.SeoData, config =>
            {
                config.Property(b => b.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("MetaDescription");

                config.Property(b => b.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("MetaTitle");

                config.Property(b => b.MetaKeyWords)
                .HasMaxLength(500)
                .HasColumnName("MetaKeyWords");

                config.Property(b => b.Canonical)
                .HasMaxLength(500)
                .HasColumnName("Canonical");
            });
        });
       
        base.OnModelCreating(modelBuilder);
    }
}
