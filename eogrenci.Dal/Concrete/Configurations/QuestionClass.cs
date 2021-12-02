using System;
using eogrenci.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eogrenci.Dal.Concrete.Configurations
{
    public class QuestionClass : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.QuestionText).HasMaxLength(500);
            builder.Property(x => x.QuestionText).IsRequired();
        }
    }
}
