namespace EfCoreDto.Core.Services;

public interface IVehicleService
{
	Task<Result<VehicleDTO>> AddVehicleAsync(string vin, int personId);

	Task<Result<VehicleDTO>> GetVehicleByVinAsync(string vin);

	Task<Result<VehicleDTO>> GetVehicleByIdAsync(int id);

	Task<Result<OwnerDTO>> GetCurrentOwnerByVinAsync(string vin);

	Task<Result<OwnerDTO>> SetCurrentOwnerAsync(string vin, int personId);
}
