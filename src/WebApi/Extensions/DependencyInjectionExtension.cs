namespace EfCoreDto.WebApi.Extensions;

public static class DependencyInjectionExtension
{
	public static void AddWebAppServices(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSwaggerTools();

		builder.Services.AddEndpoints(typeof(Program).Assembly);
	}
}
