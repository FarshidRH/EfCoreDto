namespace EfCoreDto.WebApi.Extensions;

public static class MiddlewareExtension
{
	public static void ConfigureMiddlewares(this WebApplication app)
	{
		app.UseHttpsRedirection();
		app.MapEndpoints();
		app.UseSwaggerTools();
	}

	private static void MapEndpoints(this WebApplication app)
	{
		var apiVersionSet = app.NewApiVersionSet()
			.HasApiVersion(Versions._1_0)
			.HasApiVersion(Versions._2_0)
			.Build();

		RouteGroupBuilder versionedGroup = app
			.MapGroup("api/v{version:apiVersion}")
			.WithApiVersionSet(apiVersionSet);

		foreach (IEndpointBase endpoint in
			app.Services.GetRequiredService<IEnumerable<IEndpointBase>>())
		{
			endpoint.MapEndpoint(versionedGroup);
		}
	}

	private static void UseSwaggerTools(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				foreach (string apiVersion in app.DescribeApiVersions().Select(x => x.GroupName))
				{
					string url = $"/swagger/{apiVersion}/swagger.json";
					string name = apiVersion.ToUpperInvariant();

					options.SwaggerEndpoint(url, name);
				}
			});
		}
	}
}
