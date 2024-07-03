using System.Reflection;
using Asp.Versioning;
using EfCoreDto.Infrastructure.Extensions;
using EfCoreDto.WebApi.ExceptionHandlers;
using EfCoreDto.WebApi.OpenApi;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Templates.Themes;
using SerilogTracing.Expressions;

namespace EfCoreDto.WebApi.Extensions;

internal static class DependencyInjectionExtension
{
	public static void AddWebApiServices(this WebApplicationBuilder builder)
	{
		IServiceCollection services = builder.Services;
		Assembly assembly = typeof(Program).Assembly;

		builder.AddSerilog();
		builder.AddInfrastructureServices();
		services.AddProblemDetails();
		services.AddExceptionHandlers();
		builder.AddHealthChecks();
		services.AddSwaggerTools();
		services.AddApiVersioning();
		services.AddEndpoints(assembly);
		services.AddValidators(assembly);
	}

	private static void AddSerilog(this WebApplicationBuilder builder) =>
		builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
		{
			loggerConfiguration.Enrich.WithProperty("Application", "EfCoreDto");
			loggerConfiguration.WriteTo.Console(Formatters.CreateConsoleTextFormatter(TemplateTheme.Code));
			loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
		});

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

	private static void AddHealthChecks(this WebApplicationBuilder builder)
	{
		IHealthChecksBuilder healthChecksBuilder = builder.Services.AddHealthChecks();
		healthChecksBuilder.AddCheck("self", () => HealthCheckResult.Healthy(), [HealthCheckTags.Api]);
		builder.AddInfrastructureHealthChecks(healthChecksBuilder);
	}

	private static void AddSwaggerTools(this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		services.ConfigureOptions<ConfigureSwaggerGenOptions>();
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

	private static void AddEndpoints(this IServiceCollection services, Assembly assembly)
	{
		ServiceDescriptor[] serviceDescriptors = assembly
			.DefinedTypes
			.Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpointBase)))
			.Select(type => ServiceDescriptor.Transient(typeof(IEndpointBase), type))
			.ToArray();

		services.TryAddEnumerable(serviceDescriptors);
	}

	private static void AddValidators(this IServiceCollection services, Assembly assembly)
	{
		services.AddValidatorsFromAssembly(assembly);
	}
}
