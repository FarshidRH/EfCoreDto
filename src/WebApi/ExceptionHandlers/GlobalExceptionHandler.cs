using Microsoft.AspNetCore.Diagnostics;

namespace EfCoreDto.WebApi.ExceptionHandlers;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
	private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) =>
		await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
		{
			HttpContext = httpContext,
			ProblemDetails = new ProblemDetails
			{
				Status = Status500InternalServerError,
				Detail = exception.GetType().Name,
				Extensions = { { "errors", (string[])[exception.Message] }, },
			},
			Exception = exception,
		});
}
