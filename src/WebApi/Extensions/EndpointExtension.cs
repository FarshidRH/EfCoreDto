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
			.Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpoint)))
			.Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
			.ToArray();

		services.TryAddEnumerable(serviceDescriptors);

		return services;
	}

	public static IApplicationBuilder MapEndpoints(this WebApplication app)
	{
		foreach (IEndpoint endpoint in app.Services.GetRequiredService<IEnumerable<IEndpoint>>())
		{
			endpoint.MapEndpoint(app);
		}

		return app;
	}
}
