namespace EfCoreDto.WebApi.Endpoints.Vehicle;

public class SetOwner : IEndpoint
{
	public static string EndpointName => "SetCurrentOwner";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapPost("vehicle/{vin}/owner/{personId:int}", SetCurrentOwnerAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.VehicleOwner }],
			Summary = EndpointName,
			Description = "Add new owner for vehicle.",
		});

	public static async Task<Results<CreatedAtRoute<OwnerDTO>, BadRequest<string>>> SetCurrentOwnerAsync(
		string vin, int personId, IVehicleService vehicleService)
	{
		Result<OwnerDTO> result = await vehicleService.SetCurrentOwnerAsync(vin, personId);

		if (result.IsFailure)
		{
			return TypedResults.BadRequest(result.Error);
		}

		OwnerDTO newOwner = result.Value()!;
		return TypedResults.CreatedAtRoute(newOwner, GetOwner.EndpointName, new { vin });
	}
}
