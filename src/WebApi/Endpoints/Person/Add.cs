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
		CancellationToken cancellationToken /* only for testing of CancellationToken. */)
	{
		var validationResult = await validator.ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid)
		{
			return TypedResults.ValidationProblem(validationResult.ToDictionary());
		}

		Result<PersonDTO> result =
			await personService.AddPersonAsync(request.FirstName, request.LastName, cancellationToken);

		if (result.IsFailure)
		{
			return TypedResults.Problem(result.ToProblem());
		}

		PersonDTO newPerson = result.Value()!;
		return TypedResults.CreatedAtRoute(newPerson, GetById.EndpointName, new { id = newPerson.Id });
	}
}
