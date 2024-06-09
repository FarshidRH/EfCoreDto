namespace EfCoreDto.Core.Exceptions;

public class DuplicateOwnerException : BaseException
{
	public DuplicateOwnerException()
		: this(OwnerErrors.DuplicateOwner)
	{ }

	private DuplicateOwnerException(Error error)
		: base(error)
	{
	}
}
