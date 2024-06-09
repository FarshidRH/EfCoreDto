namespace EfCoreDto.WebApi.Endpoints.Person;

public class GetById : IEndpoint
{
	public static string EndpointName => "GetPersonById";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapGet("person/{id:int}", GetPersonByIdAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.Person }],
			Summary = EndpointName,
			Description = "Get person by id.",
		});

	public static async Task<Results<Ok<PersonDTO>, ProblemHttpResult>> GetPersonByIdAsync(
		int id, IPersonService personService)
	{
		Result<PersonDTO> result = await personService.GetPersonByIdAsync(id);

		return result.IsSuccess
			? TypedResults.Ok(result.Value())
			: TypedResults.Problem(result.ToProblem());
	}
}
