namespace EfCoreDto.WebApi.Endpoints;

public interface IEndpointBase
{
	void MapEndpoint(IEndpointRouteBuilder endpointRouteBuilder);
}

public interface IEndpoint : IEndpointBase
{
	static abstract string EndpointName { get; }
}
