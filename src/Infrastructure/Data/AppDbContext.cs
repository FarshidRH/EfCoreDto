namespace EfCoreDto.Infrastructure.Data;

internal sealed class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(
			EfCoreDto.Infrastructure.AssemblyReference.Assembly);

		base.OnModelCreating(modelBuilder);
	}
}
