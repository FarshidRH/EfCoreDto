using Microsoft.AspNetCore.Diagnostics;

namespace EfCoreDto.WebApi.ExceptionHandlers;

public class BaseExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
	private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) =>
		exception is BaseException baseException &&
		await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
		{
			HttpContext = httpContext,
			ProblemDetails = baseException.Error.ToProblem(),
			Exception = exception,
		});
}
