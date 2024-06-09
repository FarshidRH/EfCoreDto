namespace EfCoreDto.WebApi.Endpoints.Vehicle;

public class GetOwner : IEndpoint
{
	public static string EndpointName => "GetCurrentOwnerByVin";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapGet("vehicle/{vin}/owner", GetCurrentOwnerByVinAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.VehicleOwner }],
			Summary = EndpointName,
			Description = "Get vehicle's current owner by VIN.",
		});

	public static async Task<Results<Ok<OwnerDTO>, ProblemHttpResult>> GetCurrentOwnerByVinAsync(
		string vin, IVehicleService vehicleService)
	{
		Result<OwnerDTO> result = await vehicleService.GetCurrentOwnerByVinAsync(vin);

		return result.IsSuccess
			? TypedResults.Ok(result.Value())
			: TypedResults.Problem(result.ToProblem());
	}
}
