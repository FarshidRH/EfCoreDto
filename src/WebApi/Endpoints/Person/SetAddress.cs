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

	public static async Task<Results<CreatedAtRoute<AddressDTO>, IResult>> SetPersonAddressAsync(
		int personId,
		SetPersonAddressRequest addressRequest,
		IValidator<SetPersonAddressRequest> validator,
		IPersonService personService)
	{
		var validationResult = await validator.ValidateAsync(addressRequest);
		if (!validationResult.IsValid)
		{
			return TypedResults.ValidationProblem(validationResult.ToDictionary());
		}

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

