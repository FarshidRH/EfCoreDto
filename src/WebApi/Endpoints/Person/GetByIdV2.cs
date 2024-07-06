namespace EfCoreDto.WebApi.Endpoints.Person;

public class GetByIdV2 : IEndpoint
{
	public static string EndpointName => "GetPersonById_V2";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapGet("person/{id:int}", GetPersonByIdAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.Person }],
			Summary = EndpointName,
			Description = "Get person by id.",
		})
		.MapToApiVersion(Versions._2_0);

	public static async Task<Results<Ok<PersonDTO>, IResult>> GetPersonByIdAsync(
		int id, ISender sender)
	{
		GetPersonByIdQuery query = new(id);
		Result<PersonDTO> result = await sender.Send(query);

		return result.IsSuccess
			? TypedResults.Ok(result.Value())
			: TypedResults.Problem(result.ToProblem());
	}
}
