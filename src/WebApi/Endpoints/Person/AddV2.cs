namespace EfCoreDto.WebApi.Endpoints.Person;

public class AddV2 : IEndpoint
{
	public static string EndpointName => "AddPerosn_V2";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapPost("person", AddPersonAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.Person }],
			Summary = EndpointName,
			Description = "Add new person.",
		})
		.MapToApiVersion(Versions._2_0);

	public static async Task<Results<CreatedAtRoute<PersonDTO>, IResult>> AddPersonAsync(
		AddPersonCommand request,
		ISender sender,
		CancellationToken cancellationToken /* only for testing of CancellationToken. */)
	{
		Result<PersonDTO> result = await sender.Send(request, cancellationToken);

		if (result.IsFailure)
		{
			return TypedResults.Problem(result.ToProblem());
		}

		PersonDTO newPerson = result.Value()!;
		return TypedResults.CreatedAtRoute(newPerson, GetById.EndpointName, new { id = newPerson.Id });
	}
}
