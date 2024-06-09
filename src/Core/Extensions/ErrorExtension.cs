namespace EfCoreDto.Core.Extensions;

public static class ErrorExtension
{
	public static Error ToError(this Exception exception) =>
		exception is BaseException baseException ? baseException.Error : Error.Failure(exception.Message);
}
