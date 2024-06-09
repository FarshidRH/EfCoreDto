namespace EfCoreDto.Core.Common;

public record Error()
{
	private Error(string detail, ErrorType type) : this()
	{
		this.Detail = detail;
		this.Type = type;
	}

	public static Error Failure(string detail) => new(detail, ErrorType.Failure);

	public static Error Invalid(string detail) => new(detail, ErrorType.Invalid);

	public static Error NotFound(string detail) => new(detail, ErrorType.NotFound);

	public static Error Conflict(string detail) => new(detail, ErrorType.Conflict);

	public string Detail { get; }

	public ErrorType Type { get; }
}
