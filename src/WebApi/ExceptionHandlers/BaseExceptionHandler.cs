using Microsoft.AspNetCore.Diagnostics;

namespace EfCoreDto.WebApi.ExceptionHandlers;

public class BaseExceptionHandler(
	IProblemDetailsService problemDetailsService,
	ILogger<BaseExceptionHandler> logger) : IExceptionHandler
{
	private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;
	private readonly ILogger<BaseExceptionHandler> _logger = logger;

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		if (exception is not BaseException baseException)
		{
			return false;
		}

		_logger.LogError(exception, "Exception occured {@Message}", baseException.Error);

		return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
		{
			HttpContext = httpContext,
			ProblemDetails = baseException.Error.ToProblem(),
			Exception = exception,
		});
	}
}
