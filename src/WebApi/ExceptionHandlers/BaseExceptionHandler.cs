using Microsoft.AspNetCore.Diagnostics;

namespace EfCoreDto.WebApi.ExceptionHandlers;

public class BaseExceptionHandler(
	IProblemDetailsService problemDetailsService,
	ILogger<BaseExceptionHandler> logger) : IExceptionHandler
{
	private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;
	private readonly ILogger<BaseExceptionHandler> _logger = logger;

	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
		if (exception is not BaseException baseException)
		{
			return false;
		}

		_logger.LogError("Error occured {@Error}", baseException.Error);

		ProblemDetails problemDetails = baseException.Error.ToProblem();
		httpContext.Response.StatusCode = problemDetails.Status!.Value;

		return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
		{
			HttpContext = httpContext,
			ProblemDetails = problemDetails,
			Exception = baseException,
		});
	}
}
