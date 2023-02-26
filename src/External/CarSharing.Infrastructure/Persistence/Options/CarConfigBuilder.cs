using System;
using CarSharing.Domain.Fleet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSharing.Infrastructure.Persistence.Options
{
    public class CarConfigBuilder : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LicenseNumber).HasMaxLength(50);
            builder.HasIndex(x => x.LicenseNumber).IsUnique();
            builder.Property(x => x.Available).IsConcurrencyToken();
        }
    }
}

