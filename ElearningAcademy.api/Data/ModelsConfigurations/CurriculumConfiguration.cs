using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElearningAcademy.api.Data.ModelsConfigurations
{
    public class CurriculumConfiguration : IEntityTypeConfiguration<Curriculum>
    {
        public void Configure(EntityTypeBuilder<Curriculum> builder)
        {
            builder.Property(c => c.Title)
                    .HasMaxLength(200);

            builder.Property(c => c.Summary)
                    .HasMaxLength(2000);
        }
    }
}
