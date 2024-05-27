using System.Reflection;
using EfCoreDto.WebApi.Endpoints;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EfCoreDto.WebApi.Extensions;

internal static class EndpointExtension
{
	public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
	{
		ServiceDescriptor[] serviceDescriptors = assembly
			.DefinedTypes
			.Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpointBase)))
			.Select(type => ServiceDescriptor.Transient(typeof(IEndpointBase), type))
			.ToArray();

		services.TryAddEnumerable(serviceDescriptors);

		return services;
	}

	public static IApplicationBuilder MapEndpoints(this WebApplication app)
	{
		foreach (IEndpointBase endpoint in app.Services.GetRequiredService<IEnumerable<IEndpointBase>>())
		{
			endpoint.MapEndpoint(app);
		}

		return app;
	}
}
