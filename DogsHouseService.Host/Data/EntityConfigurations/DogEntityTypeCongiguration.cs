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

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .UseHiLo();

            builder.Property(ci => ci.Name)
                .IsRequired();

            builder.Property(cb => cb.Color)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cb => cb.TailLength)
                .IsRequired();

            builder.Property(cb => cb.Weight)
                .IsRequired();
        }
    }
}
