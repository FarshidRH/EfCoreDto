namespace EfCoreDto.Core.DTOs;

public record VehicleDto(string VIN, OwnerDto? Owner, OwnerDto[] PreviousOwners);
