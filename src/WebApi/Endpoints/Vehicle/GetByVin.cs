namespace EfCoreDto.WebApi.Endpoints.Vehicle;

public class GetByVin : IEndpoint
{
	public static string EndpointName => "GetVehicleByVin";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapGet("vehicle/{vin}", GetVehicleByVinAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.Vehicle }],
			Summary = EndpointName,
			Description = "Get vehicle by VIN.",
		});

	public static async Task<Results<Ok<VehicleDTO>, NotFound<string>>> GetVehicleByVinAsync(
		string vin, IVehicleService vehicleService)
	{
		Result<VehicleDTO> result = await vehicleService.GetVehicleByVinAsync(vin);

		return result.IsSuccess
			? TypedResults.Ok(result.Value())
			: TypedResults.NotFound(result.Error);
	}
}
