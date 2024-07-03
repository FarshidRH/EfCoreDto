namespace EfCoreDto.WebApi.Endpoints.Person;

public class Add : IEndpoint
{
	public static string EndpointName => "AddPerosn";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapPost("person", AddPersonAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.Person }],
			Summary = EndpointName,
			Description = "Add new person.",
		});

	public static async Task<Results<CreatedAtRoute<PersonDTO>, IResult>> AddPersonAsync(
		AddPersonRequest request,
		IValidator<AddPersonRequest> validator,
		IPersonService personService,
		ILogger<Add> logger,
		CancellationToken cancellationToken /* only for testing of CancellationToken. */)
	{
		logger.LogInformation("Processing request {ReaquestName}", EndpointName);

		var validationResult = await validator.ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid)
		{
			var validationErrors = validationResult.ToDictionary();
			logger.LogError("Invalid request {RequestName} {@Error}", EndpointName, validationErrors);
			return TypedResults.ValidationProblem(validationErrors);
		}

		Result<PersonDTO> result =
			await personService.AddPersonAsync(request.FirstName, request.LastName, cancellationToken);

		if (result.IsFailure)
		{
			using (LogContext.PushProperty("Error", result.Error, true))
			{
				logger.LogError("Completed request {RequestName} with error", EndpointName);
			}
			return TypedResults.Problem(result.ToProblem());
		}

		logger.LogInformation("Completed request {ReaquestName}", EndpointName);
		PersonDTO newPerson = result.Value()!;
		return TypedResults.CreatedAtRoute(newPerson, GetById.EndpointName, new { id = newPerson.Id });
	}
}
