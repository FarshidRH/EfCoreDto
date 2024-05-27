namespace EfCoreDto.WebApi.Endpoints.Vehicle;

public class GetById : IEndpoint
{
	public static string EndpointName => "GetVehicleById";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapGet("vehicle/{id:int}", GetVehicleByIdAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.Vehicle }],
			Summary = EndpointName,
			Description = "Get vehicle by id.",
		});

	public static async Task<Results<Ok<VehicleDTO>, NotFound<string>>> GetVehicleByIdAsync(
		int id, IVehicleService vehicleService)
	{
		Result<VehicleDTO> result = await vehicleService.GetVehicleByIdAsync(id);

		return result.IsSuccess
			? TypedResults.Ok(result.Value())
			: TypedResults.NotFound(result.Error);
	}
}
