namespace EfCoreDto.WebApi.Extensions;

internal static class DependencyInjectionExtension
{
	public static void AddWebApiServices(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSwaggerTools();

		builder.Services.AddEndpoints(typeof(Program).Assembly);
	}
}
