namespace EfCoreDto.Core.Services;

public interface IVehicleService
{
	Task<Result<VehicleDto>> AddVehicleAsync(string vin, int personId);
	Task<Result<VehicleDto>> GetVehicleByVinAsync(string vin);
	Task<Result<VehicleDto>> GetVehicleByIdAsync(int id);
	Task<Result<OwnerDto>> GetCurrentOwnerByVinAsync(string vin);
	Task<Result<OwnerDto>> SetCurrentOwnerAsync(string vin, int personId);
}
