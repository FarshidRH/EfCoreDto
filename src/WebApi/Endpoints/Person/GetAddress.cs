
namespace EfCoreDto.WebApi.Endpoints.Person;

public class GetAddress : IEndpoint
{
	public static string EndpointName => "GetPersonAddresses";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapGet("person/{personId:int}/address", GetPersonAddressesAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.Address }],
			Summary = EndpointName,
			Description = "Get all addresses of person.",
		});

	public static async Task<Results<Ok<AddressDTO[]>, ProblemHttpResult>> GetPersonAddressesAsync(
		int personId, IPersonService personService)
	{
		Result<AddressDTO[]> result = await personService.GetPersonsAddressesAsync(personId);

		return result.IsSuccess
			? TypedResults.Ok(result.Value())
			: TypedResults.Problem(result.ToProblem());
	}
}
