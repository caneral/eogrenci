using System;
using eogrenci.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eogrenci.Dal.Concrete.Configurations
{
    public class LessonClass : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(250);

            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(250);

            builder.Property(x => x.DepartmentId).IsRequired();
            builder.HasOne<Department>(x => x.Department).WithMany(x => x.Lessons).HasForeignKey(x => x.DepartmentId);

        }
    }
}
