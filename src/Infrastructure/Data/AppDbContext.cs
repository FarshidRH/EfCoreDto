namespace EfCoreDto.Infrastructure.Data;

public sealed class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
		base.OnModelCreating(modelBuilder);
	}
}
