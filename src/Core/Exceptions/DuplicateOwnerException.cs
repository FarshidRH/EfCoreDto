namespace EfCoreDto.Core.Exceptions;

public class DuplicateOwnerException : Exception
{
	public DuplicateOwnerException()
	{ }

	public DuplicateOwnerException(string? message)
		: base(message)
	{ }

	public DuplicateOwnerException(string? message, Exception? innerException)
		: base(message, innerException)
	{ }
}
