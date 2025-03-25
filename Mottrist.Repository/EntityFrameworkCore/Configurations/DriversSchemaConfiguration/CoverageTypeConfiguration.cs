using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.DriversSchemaConfiguration
{
    internal class CoverageTypeConfiguration : IEntityTypeConfiguration<CoverageType>
    {
        public void Configure(EntityTypeBuilder<CoverageType> builder)
        {

            builder.ToTable("CoverageTypes", schema: "Drivers");
        }
    }
}
