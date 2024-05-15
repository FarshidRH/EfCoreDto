namespace EfCoreDto.Infrastructure.Data.Configurations;

internal class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
	public void Configure(EntityTypeBuilder<Owner> builder)
	{
		builder.ToTable("vehicle_owner");

		builder.Property<int>("vehicle_id");

		builder.HasOne<Person>("_person")
			.WithMany()
			.HasForeignKey("person_id");

		builder.Navigation("_person").AutoInclude();

		builder.Property(x => x.From)
			.HasColumnName("from_date");

		builder.Property(x => x.To)
			.HasColumnName("to_date");

		builder.HasKey("vehicle_id", "person_id");
	}
}
