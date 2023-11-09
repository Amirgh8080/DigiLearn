using CoreModule.Domain.Categories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistant.Category;

public class CategoryConfig : IEntityTypeConfiguration<CourseCategory>
{
    public void Configure(EntityTypeBuilder<CourseCategory> builder)
    {
        builder.ToTable("categories");
        builder.HasIndex(p => p.Slug).IsUnique();


        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.HasMany<CourseCategory>()
            .WithOne().OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(r => r.ParentId);

    }
}
