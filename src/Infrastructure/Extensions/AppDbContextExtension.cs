namespace EfCoreDto.Infrastructure.Extensions;

public static class AppDbContextExtension
{
	public static async Task<Person?> PersonWithIdAsync(this AppDbContext dbContext, int id, bool asNoTracking = false)
	{
		IQueryable<Person> query = dbContext.Set<Person>();

		if (asNoTracking)
		{
			query = query.AsNoTracking();
		}

		return await query.SingleOrDefaultAsync(x => EF.Property<int>(x, "_id") == id);
	}

	public static async Task<Vehicle?> VehicleWithVinAsync(this AppDbContext dbContext, VIN vin, bool asNoTracking = false)
	{
		IQueryable<Vehicle> query = dbContext.Set<Vehicle>().AsSplitQuery();

		if (asNoTracking)
		{
			query = query.AsNoTracking();
		}

		return await query.SingleOrDefaultAsync(x => x.VIN == vin);
	}

	public static async Task<Vehicle?> VehicleWithIdAsync(this AppDbContext dbContext, int id, bool asNoTracking = false)
	{
		IQueryable<Vehicle> query = dbContext.Set<Vehicle>().AsSplitQuery();

		if (asNoTracking)
		{
			query = query.AsNoTracking();
		}

		return await query.SingleOrDefaultAsync(x => EF.Property<int>(x, "id") == id);
	}
}
