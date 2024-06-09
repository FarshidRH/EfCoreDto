namespace EfCoreDto.Core.Exceptions;

public class InvalidVinException : BaseException
{
	public InvalidVinException()
		: this(VehicleErrors.InvalidVin)
	{ }

	private InvalidVinException(Error error)
		: base(error)
	{ }
}
