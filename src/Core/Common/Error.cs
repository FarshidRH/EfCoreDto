namespace EfCoreDto.Core.Common;

public record Error(string Message, ErrorType Type)
{
	public static Error Failure(string message) => new(message, ErrorType.Failure);

	public static Error Invalid(string message) => new(message, ErrorType.Invalid);

	public static Error NotFound(string message) => new(message, ErrorType.NotFound);

	public static Error Conflict(string message) => new(message, ErrorType.Conflict);
}
