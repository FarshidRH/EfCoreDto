using Microsoft.OpenApi.Models;

namespace EfCoreDto.WebApi.Extensions;

public static class DependencyInjectionExtension
{
	public static void AddWebAppServices(this IHostApplicationBuilder builder)
	{
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(opt =>
		{
			opt.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "EfCoreDto API",
				Description = "API for EfCoreDto project",
				Version = "1.0",
			});
		});

		builder.Services.AddEndpoints(typeof(Program).Assembly);
	}
}
