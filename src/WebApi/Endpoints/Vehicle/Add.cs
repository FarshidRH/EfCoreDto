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

	public static async Task<Results<CreatedAtRoute<VehicleDTO>, IResult>> AddVehicleAsync(
		AddVehicleRequest request,
		IValidator<AddVehicleRequest> validator,
		IVehicleService vehicleService)
	{
		var validationResult = await validator.ValidateAsync(request);
		if (!validationResult.IsValid)
		{
			return TypedResults.ValidationProblem(validationResult.ToDictionary());
		}

		Result<VehicleDTO> result = await vehicleService.AddVehicleAsync(request.Vin, request.PersonId);

		if (result.IsFailure)
		{
			return TypedResults.Problem(result.ToProblem());
		}

		VehicleDTO newVehicle = result.Value()!;
		return TypedResults.CreatedAtRoute(newVehicle, GetByVin.EndpointName, new { vin = newVehicle.VIN });
	}
}
