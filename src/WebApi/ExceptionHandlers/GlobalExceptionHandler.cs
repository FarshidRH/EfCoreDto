using Microsoft.AspNetCore.Diagnostics;

namespace EfCoreDto.WebApi.ExceptionHandlers;

public class GlobalExceptionHandler(
	IProblemDetailsService problemDetailsService,
	ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;
	private readonly ILogger<GlobalExceptionHandler> _logger = logger;

	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
		_logger.LogError(exception, "Exception occured: {Error}", exception.Message);

		ProblemDetails problemDetails = new()
		{
			Status = Status500InternalServerError,
			Detail = exception.GetType().Name,
			Extensions = { { "errors", (string[])[exception.Message] }, },
		};

		return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
		{
			HttpContext = httpContext,
			ProblemDetails = problemDetails,
			Exception = exception,
		});
	}
}
