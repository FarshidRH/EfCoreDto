namespace EfCoreDto.Infrastructure.Data.Configurations;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
	public void Configure(EntityTypeBuilder<Address> builder)
	{
		builder.ToTable("address");

		builder.Property<int>("id");
		builder.Property<string>("type").HasMaxLength(16);

		builder.HasDiscriminator<string>("type")
			.HasValue<DeliveryAddress>(nameof(DeliveryAddress))
			.HasValue<InvoiceAddress>(nameof(InvoiceAddress));

		builder.Property(x => x.AddressLine1).HasColumnName("address_line_1");
		builder.Property(x => x.AddressLine2).HasColumnName("address_line_2");
		builder.Property(x => x.PostalCode).HasColumnName("postal_code").HasMaxLength(10);
		builder.Property(x => x.City).HasColumnName("city").HasMaxLength(128);
		builder.Property(x => x.Country).HasColumnName("country").HasMaxLength(128);
		builder.Property(x => x.IsCurrent).HasColumnName("is_current");

		builder.HasKey("id");
	}
}
