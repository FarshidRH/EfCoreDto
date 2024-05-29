namespace EfCoreDto.Infrastructure.Data.Configurations;

internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
	public void Configure(EntityTypeBuilder<Vehicle> builder)
	{
		builder.ToTable("vehicle");

		builder.Property<int>("id");

		builder.HasMany<Owner>("_owners").WithOne().HasForeignKey("vehicle_id");
		builder.Navigation("_owners").AutoInclude();

		builder.Property(x => x.VIN)
			.HasColumnName("vin")
			.HasMaxLength(17)
			.HasConversion(x => x.Value, x => VIN.Create(x));

		builder.Ignore(x => x.CurrentOwner);
		builder.Ignore(x => x.PreviousOwners);

		builder.HasKey("id");
	}
}
