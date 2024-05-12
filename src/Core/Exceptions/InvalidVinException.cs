namespace EfCoreDto.Core.Exceptions;

public class InvalidVinException : Exception
{
	public InvalidVinException()
	{ }

	public InvalidVinException(string? message)
		: base(message)
	{ }

	public InvalidVinException(string? message, Exception? innerException)
		: base(message, innerException)
	{ }
}
