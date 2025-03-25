using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations
{
    public class TravellerConfiguration : IEntityTypeConfiguration<Traveller>
    {
        public void Configure(EntityTypeBuilder<Traveller> builder)
        {
            builder.HasKey(t => t.Id);


        }
    }

}