using System;
using CarSharing.Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSharing.Infrastructure.Persistence.Options
{
    public class BookingConfigBuilder : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CarId);
            builder.Property(x => x.UserId);
            builder.Property(x => x.StartDate);
            builder.Property(x => x.EndDate);
            builder.HasIndex(x => x.CarId);
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.StartDate);
        }
    }
}

