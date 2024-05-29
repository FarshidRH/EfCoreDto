namespace EfCoreDto.WebApi.Extensions;

public static class MiddlewareExtension
{
	public static void ConfigureMiddlewares(this WebApplication app)
	{
		app.UseHttpsRedirection();
		app.UseSwaggerTools();
		app.MapEndpoints();
	}

	private static void UseSwaggerTools(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(opt =>
				opt.SwaggerEndpoint("/swagger/v1/swagger.json", "EfCoreDto API v1"));
		}
	}

	private static void MapEndpoints(this WebApplication app)
	{
		foreach (IEndpointBase endpoint in
			app.Services.GetRequiredService<IEnumerable<IEndpointBase>>())
		{
			endpoint.MapEndpoint(app);
		}
	}
}
