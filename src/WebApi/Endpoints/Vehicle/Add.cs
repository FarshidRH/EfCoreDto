namespace EfCoreDto.WebApi.Endpoints.Vehicle;

public class Add : IEndpoint
{
	public static string EndpointName => "AddVehicle";

	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder) => endpointRouteBuilder
		.MapPost("vehicle", AddVehicleAsync)
		.WithName(EndpointName)
		.WithOpenApi(config => new(config)
		{
			Tags = [new() { Name = Tags.Vehicle }],
			Summary = EndpointName,
			Description = "Add new vehicle.",
		});

	public static async Task<Results<CreatedAtRoute<VehicleDTO>, ProblemHttpResult>> AddVehicleAsync(
		AddVehicleRequest request,
		IVehicleService vehicleService)
	{
		Result<VehicleDTO> result = await vehicleService.AddVehicleAsync(request.Vin, request.PersonId);

		if (result.IsFailure)
		{
			return TypedResults.Problem(result.ToProblem());
		}

		VehicleDTO newVehicle = result.Value()!;
		return TypedResults.CreatedAtRoute(newVehicle, GetByVin.EndpointName, new { vin = newVehicle.VIN });
	}
}

public record AddVehicleRequest(string Vin, int PersonId);
