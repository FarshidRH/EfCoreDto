namespace EfCoreDto.Core.Errors;

public static class VehicleErrors
{
	public static readonly Error InvalidVin = Error.Invalid("Invalid VIN.");

	public static readonly Error DuplicateVin = Error.Conflict("Duplicate VIN.");

	public static readonly Error VehicleNotFound = Error.NotFound("Vehicle not found.");
}
