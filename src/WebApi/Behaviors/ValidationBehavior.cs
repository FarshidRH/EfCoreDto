using FluentValidation.Results;

namespace EfCoreDto.WebApi.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
	: IPipelineBehavior<TRequest, TResponse>
	where TRequest : class
{
	private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		if (_validators.Any())
		{
			ValidationContext<TRequest> validationContext = new(request);

			ValidationResult[] validationResults = await Task.WhenAll(
				_validators.Select(v => v.ValidateAsync(validationContext, cancellationToken)));

			List<ValidationFailure> validationFailures = validationResults
				.Where(r => !r.IsValid)
				.SelectMany(r => r.Errors)
				.ToList();

			if (validationFailures.Count > 0)
			{
				throw new ValidationException(validationFailures);
			}
		}

		return await next();
	}
}
