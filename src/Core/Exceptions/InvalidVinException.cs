namespace EfCoreDto.Core.Exceptions;

public class InvalidVinException : Exception
{
	public InvalidVinException()
		: this(VehicleErrors.InvalidVin)
	{ }

	private InvalidVinException(string? message)
		: this(message, null)
	{ }

	private InvalidVinException(string? message, Exception? innerException)
		: base(message, innerException)
	{ }
}
