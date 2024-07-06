using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;

namespace EfCoreDto.WebApi.ExceptionHandlers;

public class ValidationExceptionHandler(
	IProblemDetailsService problemDetailsService,
	ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
{
	private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;
	private readonly ILogger<ValidationExceptionHandler> _logger = logger;

	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
		if (exception is not ValidationException validationException)
		{
			return false;
		}

		using (LogContext.PushProperty("Errors", validationException.Errors))
		{
			_logger.LogError("One or more validation errors occured.");
		}

		var problemDetails = new ValidationProblemDetails
		{
			Status = StatusCodes.Status400BadRequest,
			Errors = new ValidationResult(validationException.Errors).ToDictionary(),
			//Extensions = { { "errors", validationException.Errors.Select(e => e.ErrorMessage) } },
		};
		httpContext.Response.StatusCode = problemDetails.Status!.Value;

		return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
		{
			HttpContext = httpContext,
			ProblemDetails = problemDetails,
			Exception = validationException,
		});
	}
}
