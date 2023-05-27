using CoreModule.Domain.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistant.Course;

public class CourseConfig : IEntityTypeConfiguration<Domain.Course.Models.Course>
{
    public void Configure(EntityTypeBuilder<Domain.Course.Models.Course> builder)
    {
        builder.ToTable("Courses", "course");
        builder.HasIndex(p => p.Slug).IsUnique();


        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(p => p.ImageName)
            .IsRequired()
            .HasMaxLength(100); 
        
        builder.Property(p => p.TrailerName)
            .IsRequired()
            .HasMaxLength(200);




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

        builder.OwnsMany(p => p.Sections, config =>
        {
            config.ToTable("Sections", "course");
            config.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(100);

            config.OwnsMany(b => b.Episodes, e =>
            {
                e.ToTable("Episodes", "course");

                e.Property(s => s.Title)
                .HasMaxLength(100);

                e.Property(s => s.EnglishTitle)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

                e.Property(s => s.VideoName)
                .IsRequired()
                .HasMaxLength(200);

                e.Property(s => s.AttachmentName)
                .IsRequired(false)
                .HasMaxLength(200);
            });
        });
    }
}
