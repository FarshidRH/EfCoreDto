namespace EfCoreDto.WebApi.Endpoints.My;

internal sealed class Bye : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
	{
		endpointRouteBuilder
			.MapGet("bye", () => TypedResults.Ok("Goodbye World!"))
			.WithTags("My Endpoints");
	}
}
