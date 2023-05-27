using CoreModule.Domain.Teacher.Models;
using CoreModule.Infrastructure.Persistant.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreModule.Infrastructure.Persistant.Teacher;

public class ITeacherConfig : IEntityTypeConfiguration<Domain.Teacher.Models.Teacher>
{

    public void Configure(EntityTypeBuilder<Domain.Teacher.Models.Teacher> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasIndex(b => b.UserName).IsUnique();
       
        builder.Property(b => b.UserName)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(25);
        builder.Property(b => b.CvFileName)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Domain.Teacher.Models.Teacher>(b => b.UserId);
    }
}
