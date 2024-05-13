namespace EfCoreDto.Core.DTOs;

public record VehicleDTO(string VIN, OwnerDTO? Owner, OwnerDTO[] PreviousOwners);
