namespace EfCoreDto.Core.Exceptions;

public abstract class BaseException : Exception
{
	protected BaseException(Error error)
		: this(message: error?.Detail)
		=> this.Error = error ?? throw new ArgumentNullException(nameof(error));

	private BaseException()
		: this(message: null)
	{ }

	private BaseException(string? message)
		: this(message, innerException: null)
	{ }

	private BaseException(string? message, Exception? innerException)
		: base(message, innerException)
	{ }

	public Error Error { get; }
}
