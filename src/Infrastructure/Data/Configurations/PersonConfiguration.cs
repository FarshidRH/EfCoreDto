namespace EfCoreDto.Infrastructure.Data.Configurations;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
	public void Configure(EntityTypeBuilder<Person> builder)
	{
		builder.ToTable("person");

		builder.Property<int>("_id").HasColumnName("id");

		builder.HasMany<Address>("_addresses").WithOne().HasForeignKey("person_id").IsRequired();
		builder.Navigation("_addresses").AutoInclude();

		builder.ComplexProperty(x => x.Name, x =>
		{
			x.Property(y => y.FirstName).HasColumnName("first_name").HasMaxLength(128);
			x.Property(y => y.LastName).HasColumnName("last_name").HasMaxLength(128);
		});

		builder.Ignore(x => x.DeliveryAddress);
		builder.Ignore(x => x.InvoiceAddress);

		builder.HasKey("_id");
	}
}
