using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EfCoreDto.WebApi.OpenApi;

public class ConfigureSwaggerGetOptions(IApiVersionDescriptionProvider provider)
	: IConfigureNamedOptions<SwaggerGenOptions>
{
	private readonly IApiVersionDescriptionProvider _provider = provider;

	public void Configure(string? name, SwaggerGenOptions options) => this.Configure(options);

	public void Configure(SwaggerGenOptions options)
	{
		foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
		{
			OpenApiInfo openApiInfo = new()
			{
				Title = "EfCoreDto API",
				Description = "API for EfCoreDto project",
				Version = description.ApiVersion.ToString(),
			};

			options.SwaggerDoc(description.GroupName, openApiInfo);
		}
	}
}
