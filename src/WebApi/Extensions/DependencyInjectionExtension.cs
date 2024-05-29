using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

namespace EfCoreDto.WebApi.Extensions;

internal static class DependencyInjectionExtension
{
	public static void AddWebApiServices(this IHostApplicationBuilder builder)
	{
		builder.AddInfrastructureServices();

		builder.Services.AddSwaggerTools();
		builder.Services.AddEndpoints(typeof(Program).Assembly);
	}

	private static void AddSwaggerTools(this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(opt => opt.SwaggerDoc("v1", new OpenApiInfo
		{
			Title = "EfCoreDto API",
			Description = "API for EfCoreDto project",
			Version = "1.0",
		}));
	}

	private static void AddEndpoints(this IServiceCollection services, Assembly assembly)
	{
		ServiceDescriptor[] serviceDescriptors = assembly
			.DefinedTypes
			.Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpointBase)))
			.Select(type => ServiceDescriptor.Transient(typeof(IEndpointBase), type))
			.ToArray();

		services.TryAddEnumerable(serviceDescriptors);
	}
}
