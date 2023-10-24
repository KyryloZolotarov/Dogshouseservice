using DogsHouseService.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DogsHouseService.Host.Data.EntityConfigurations
{
    public class DogEntityTypeCongiguration : IEntityTypeConfiguration<DogEntity>
    {
        public void Configure(EntityTypeBuilder<DogEntity> builder)
        {
            builder.ToTable("Dogs");

            builder.HasKey(ci => ci.Name)
                .IsClustered();

            builder.Property(ci => ci.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(cb => cb.Color)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cb => cb.TailLength)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(cb => cb.Weight)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}
