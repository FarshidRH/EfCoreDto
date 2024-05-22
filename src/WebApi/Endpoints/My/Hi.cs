namespace EfCoreDto.WebApi.Endpoints.My;

internal class Hi : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
	{
		endpointRouteBuilder
			.MapGet("hi", () => TypedResults.Ok("Hello World!"))
			.WithTags("My Endpoints");
	}
}
