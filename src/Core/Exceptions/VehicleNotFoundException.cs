namespace EfCoreDto.Core.Exceptions;

public class VehicleNotFoundException : BaseException
{
	public VehicleNotFoundException()
		: this(VehicleErrors.VehicleNotFound)
	{ }

	private VehicleNotFoundException(Error error)
		: base(error)
	{ }
}
