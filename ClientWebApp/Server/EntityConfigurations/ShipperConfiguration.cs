﻿namespace ClientWebApp.Server.EntityConfigurations;

public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
{
    public void Configure(EntityTypeBuilder<Shipper> builder)
    {
        builder.Property(e => e.ShipperId).HasColumnName("ShipperID");
        builder.Property(e => e.CompanyName).IsRequired().HasMaxLength(40);
        builder.Property(e => e.Phone).HasMaxLength(24);
    }
}