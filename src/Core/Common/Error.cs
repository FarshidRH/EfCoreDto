namespace EfCoreDto.Core.Common;

public record Error()
{
	private Error(string message, ErrorType type) : this()
	{
		this.Message = message;
		this.Type = type;
	}

	public static Error Failure(string message) => new(message, ErrorType.Failure);

	public static Error Invalid(string message) => new(message, ErrorType.Invalid);

	public static Error NotFound(string message) => new(message, ErrorType.NotFound);

	public static Error Conflict(string message) => new(message, ErrorType.Conflict);

	public string Message { get; }

	public ErrorType Type { get; }
}
