namespace EfCoreDto.Infrastructure.Services;

internal sealed class VehicleService(AppDbContext dbContext) : IVehicleService
{
	private readonly AppDbContext _dbContext = dbContext;

	public async Task<Result<VehicleDTO>> AddVehicleAsync(string vin, int personId)
	{
		if (!VIN.TryCreate(vin, out VIN? validVin))
		{
			return Result.Fail<VehicleDTO>(VehicleErrors.InvalidVin);
		}

		Vehicle? existing = await _dbContext.VehicleWithVinAsync(validVin!, true);
		if (existing != null)
		{
			return Result.Fail<VehicleDTO>(VehicleErrors.DuplicateVin);
		}

		Person? owner = await _dbContext.PersonWithIdAsync(personId);
		if (owner == null)
		{
			return Result.Fail<VehicleDTO>(PersonErrors.PersonNotFound);
		}

		try
		{
			var newVehicle = Vehicle.Create(validVin!, owner);
			await _dbContext.AddAsync(newVehicle);
			await _dbContext.SaveChangesAsync();

			return Result.Success(newVehicle.ToModel());
		}
		catch (Exception exception)
		{
			return Result.Fail<VehicleDTO>(exception.Message);
		}
	}

	public async Task<Result<VehicleDTO>> GetVehicleByVinAsync(string vin)
	{
		if (!VIN.TryCreate(vin, out VIN? validVin))
		{
			return Result.Fail<VehicleDTO>(VehicleErrors.InvalidVin);
		}

		Vehicle? vehicle = await _dbContext.VehicleWithVinAsync(validVin!, true);

		return vehicle != null
			? Result.Success(vehicle.ToModel())
			: Result.Fail<VehicleDTO>(VehicleErrors.VehicleNotFound);
	}

	public async Task<Result<VehicleDTO>> GetVehicleByIdAsync(int id)
	{
		Vehicle? vehicle = await _dbContext.VehicleWithIdAsync(id, true);

		return vehicle != null
			? Result.Success(vehicle.ToModel())
			: Result.Fail<VehicleDTO>(VehicleErrors.VehicleNotFound);
	}

	public async Task<Result<OwnerDTO>> GetCurrentOwnerByVinAsync(string vin)
	{
		if (!VIN.TryCreate(vin, out VIN? validVIN))
		{
			return Result.Fail<OwnerDTO>(VehicleErrors.InvalidVin);
		}

		var vehicleProjection = await _dbContext.Set<Vehicle>()
			.Where(x => x.VIN == validVIN!)
			.Select(x => new { Vin = x.VIN, Owner = x.CurrentOwner })
			.FirstOrDefaultAsync();

		if (vehicleProjection == null)
		{
			return Result.Fail<OwnerDTO>(VehicleErrors.VehicleNotFound);
		}

		if (vehicleProjection.Owner == null)
		{
			return Result.Fail<OwnerDTO>(OwnerErrors.OwnerNotFound);
		}

		return Result.Success(vehicleProjection.Owner.ToModel());
	}

	public async Task<Result<OwnerDTO>> SetCurrentOwnerAsync(string vin, int personId)
	{
		if (!VIN.TryCreate(vin, out VIN? validVIN))
		{
			return Result.Fail<OwnerDTO>(VehicleErrors.InvalidVin);
		}

		Vehicle? vehicle = await _dbContext.VehicleWithVinAsync(validVIN!);
		if (vehicle == null)
		{
			return Result.Fail<OwnerDTO>(VehicleErrors.VehicleNotFound);
		}

		Person? newOwner = await _dbContext.PersonWithIdAsync(personId);
		if (newOwner == null)
		{
			return Result.Fail<OwnerDTO>(PersonErrors.PersonNotFound);
		}

		try
		{
			_ = vehicle.SetOwner(newOwner);
			await _dbContext.SaveChangesAsync();
		}
		catch (Exception exception)
		{
			return Result.Fail<OwnerDTO>(exception.Message);
		}

		return Result.Success(vehicle.CurrentOwner!.ToModel());
	}
}
