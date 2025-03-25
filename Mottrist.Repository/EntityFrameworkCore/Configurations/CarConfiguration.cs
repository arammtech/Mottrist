using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities.CarDetails;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);


            builder.HasOne(c => c.Brand)
                   .WithMany()
                   .HasForeignKey(c => c.BrandId);

            builder.HasOne(c => c.Model)
                   .WithMany()
                   .HasForeignKey(c => c.ModelId);

            builder.HasOne(c => c.BodyType)
                   .WithMany()
                   .HasForeignKey(c => c.BodyTypeId);

            builder.HasOne(c => c.FuelType)
                   .WithMany()
                   .HasForeignKey(c => c.FuelTypeId);

        }
    }
}