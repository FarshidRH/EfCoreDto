namespace EfCoreDto.WebApi.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
	: IPipelineBehavior<TRequest, TResponse>
	where TRequest : class
	where TResponse : Result
{
	private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;
	private readonly string _requestName = typeof(TRequest).Name;

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		_logger.LogInformation("Processing request {ReaquestName}", _requestName);

		TResponse result = await next();

		if (result.IsSuccess)
		{
			_logger.LogInformation("Completed request {ReaquestName}", _requestName);
		}
		else
		{
			using (LogContext.PushProperty("Error", result.Error, true))
			{
				_logger.LogError("Completed request {RequestName} with error", _requestName);
			}
		}

		return result;
	}
}
