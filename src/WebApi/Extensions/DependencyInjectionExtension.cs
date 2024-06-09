using Asp.Versioning;
using EfCoreDto.Infrastructure.Extensions;
using EfCoreDto.WebApi.ExceptionHandlers;
using EfCoreDto.WebApi.OpenApi;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EfCoreDto.WebApi.Extensions;

internal static class DependencyInjectionExtension
{
	public static void AddWebApiServices(this IHostApplicationBuilder builder)
	{
		builder.AddInfrastructureServices();

		IServiceCollection services = builder.Services;
		services.AddProblemDetails();
		services.AddExceptionHandlers();
		services.AddSwaggerTools();
		services.AddApiVersioning();
		services.AddEndpoints();
	}

	private static void AddExceptionHandlers(this IServiceCollection services)
	{
		services.AddExceptionHandler<BaseExceptionHandler>();
		services.AddExceptionHandler<GlobalExceptionHandler>();
	}

	private static void AddProblemDetails(this IServiceCollection services) =>
		services.AddProblemDetails(config => config.CustomizeProblemDetails = context =>
		{
			context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
			context.ProblemDetails.Extensions.Add("trace-id", context.HttpContext.TraceIdentifier);
		});

	private static void AddSwaggerTools(this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		services.ConfigureOptions<ConfigureSwaggerGetOptions>();
	}

	private static void AddApiVersioning(this IServiceCollection services)
	{
		IApiVersioningBuilder apiVersioningBuilder = services.AddApiVersioning(options =>
		{
			options.DefaultApiVersion = Versions._1_0;
			options.ApiVersionReader = new UrlSegmentApiVersionReader();
			options.ReportApiVersions = true;
		});

		apiVersioningBuilder.AddApiExplorer(options =>
		{
			options.GroupNameFormat = "'v'V";
			options.SubstituteApiVersionInUrl = true;
		});
	}

	private static void AddEndpoints(this IServiceCollection services)
	{
		ServiceDescriptor[] serviceDescriptors = typeof(Program).Assembly
			.DefinedTypes
			.Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpointBase)))
			.Select(type => ServiceDescriptor.Transient(typeof(IEndpointBase), type))
			.ToArray();

		services.TryAddEnumerable(serviceDescriptors);
	}
}
