namespace EfCoreDto.Core.Errors;

public static class PersonErrors
{
	public static readonly Error PersonNotFound = Error.NotFound("Person not found.");
}
