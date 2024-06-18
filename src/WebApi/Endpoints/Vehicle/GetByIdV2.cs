namespace EfCoreDto.WebApi.Endpoints.Vehicle;

public class GetByIdV2 : IEndpoint
{
	public static string EndpointName => "GetVehicleById_V2";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapGet("vehicle/{id:int}", GetVehicleByIdAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.Vehicle }],
			Summary = EndpointName,
			Description = "Get vehicle by id.",
		})
		.MapToApiVersion(Versions._2_0);

	public static async Task<Results<Ok<VehicleDTO>, IResult>> GetVehicleByIdAsync(
		int id, IVehicleService vehicleService)
	{
		if (id < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(id), "Vehicle's id must be greater than zero.");
		}

		Result<VehicleDTO> result = await vehicleService.GetVehicleByIdAsync(id);

		if (result.IsFailure)
		{
			throw new VehicleNotFoundException();
		}

		return TypedResults.Ok(result.Value());
	}
}
