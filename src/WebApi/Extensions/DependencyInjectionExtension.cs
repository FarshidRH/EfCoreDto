using Microsoft.OpenApi.Models;

namespace EfCoreDto.WebApi.Extensions;

public static class DependencyInjectionExtension
{
	public static void AddWebAppServices(this IServiceCollection services)
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
	}
}
