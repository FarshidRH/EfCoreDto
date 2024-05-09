namespace EfCoreDto.Core.Services;

public interface IVehicleService
{
    Task<Result<VehicleDTO>> AddVehicle(string vin, int personId);
    Task<Result<VehicleDTO>> GetVehicleByVin(string vin);
    Task<Result<VehicleDTO>> GetVehicleById(int id);
    Task<Result<OwnerDTO>> GetCurrentOwnerByVin(string vin);
    Task<Result<OwnerDTO>> SetCurrentOwner(string vin, int personId);
}
