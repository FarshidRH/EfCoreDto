namespace EfCoreDto.Core.Exceptions;

public class DuplicateOwnerException : Exception
{
	public DuplicateOwnerException()
		: this(OwnerErrors.DuplicateOwner)
	{ }

	private DuplicateOwnerException(string? message)
		: this(message, null)
	{ }

	private DuplicateOwnerException(string? message, Exception? innerException)
		: base(message, innerException)
	{ }
}
