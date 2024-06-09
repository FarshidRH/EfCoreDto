namespace EfCoreDto.WebApi.Endpoints.Person;

public class SetAddress : IEndpoint
{
	public static string EndpointName => "SetPersonAddress";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapPost("person/{personId:int}/address", SetPersonAddressAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.Address }],
			Summary = EndpointName,
			Description = "Add new address for person.",
		});

	public static async Task<Results<CreatedAtRoute<AddressDTO>, ProblemHttpResult>> SetPersonAddressAsync(
		int personId,
		SetPersonAddressRequest addressRequest,
		IPersonService personService)
	{
		Result<AddressDTO> result = await personService.SetPersonsAddressAsync(
			personId,
			addressRequest.Type,
			addressRequest.AddressLine1,
			addressRequest.AddressLine2,
			addressRequest.PostalCode,
			addressRequest.City,
			addressRequest.Country);

		return result.IsSuccess
			? TypedResults.CreatedAtRoute(result.Value(), GetAddress.EndpointName, new { personId })
			: TypedResults.Problem(result.ToProblem());
	}
}

public record SetPersonAddressRequest(
	AddressType Type,
	string AddressLine1,
	string AddressLine2,
	string PostalCode,
	string City,
	string Country);
