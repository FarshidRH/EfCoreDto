namespace EfCoreDto.WebApi.Endpoints;

internal interface IEndpoint
{
	void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder);
}
