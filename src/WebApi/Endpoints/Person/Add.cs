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

	public static async Task<Results<CreatedAtRoute<PersonDTO>, BadRequest<string>>> AddPersonAsync(
		AddPersonRequest request,
		IPersonService personService,
		CancellationToken cancellationToken /* only for testing of CancellationToken. */)
	{
		Result<PersonDTO> result =
			await personService.AddPersonAsync(request.FirstName, request.LastName, cancellationToken);

		if (result.IsFailure)
		{
			return TypedResults.BadRequest(result.Error);
		}

		PersonDTO newPerson = result.Value()!;
		return TypedResults.CreatedAtRoute(newPerson, GetById.EndpointName, new { id = newPerson.Id });
	}
}

public record AddPersonRequest(string FirstName, string LastName);
