namespace EfCoreDto.Core.Errors;

public static class OwnerErrors
{
	public static readonly Error OwnerNotFound = Error.NotFound("Owner not found.");

	public static readonly Error DuplicateOwner = Error.Conflict("Duplicate Owner.");
}
