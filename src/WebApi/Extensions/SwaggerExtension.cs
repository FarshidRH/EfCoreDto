using Microsoft.OpenApi.Models;

namespace EfCoreDto.WebApi.Extensions;

internal static class SwaggerExtension
{
	public static IServiceCollection AddSwaggerTools(this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();

		services.AddSwaggerGen(opt =>
		{
			opt.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "EfCoreDto API",
				Description = "API for EfCoreDto project",
				Version = "1.0",
			});
		});

		return services;
	}

	public static IApplicationBuilder UseSwaggerTools(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(opt =>
			{
				opt.SwaggerEndpoint("/swagger/v1/swagger.json", "EfCoreDto API v1");
			});
		}

		return app;
	}
}
