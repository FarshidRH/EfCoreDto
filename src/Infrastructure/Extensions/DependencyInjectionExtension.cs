using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EfCoreDto.Infrastructure.Extensions;

public static class DependencyInjectionExtension
{
	public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
	{
		builder.Services.AddDbContext<AppDbContext>(opt => opt.ConfigureDbContextOptions(builder));

		builder.Services.AddScoped<IVehicleService, VehicleService>();
		builder.Services.AddScoped<IPersonService, PersonService>();
	}

	public static void AddInfrastructureHealthChecks(
		this IHostApplicationBuilder host, IHealthChecksBuilder healthChecksBuilder)
	{
		string[] infraTags = [HealthCheckTags.Infra];
		healthChecksBuilder.AddDbContextCheck<AppDbContext>(tags: infraTags);
		healthChecksBuilder.AddSqlServer(host.GetSqlServerConnectionString(), tags: infraTags);
	}

	private static DbContextOptionsBuilder ConfigureDbContextOptions(
		this DbContextOptionsBuilder dbContextOptions, IHostApplicationBuilder host)
	{
		dbContextOptions.UseSqlServer(
			connectionString: host.GetSqlServerConnectionString(),
			sqlServerOptionsBuilder => sqlServerOptionsBuilder.EnableRetryOnFailure());

		if (host.Environment.IsDevelopment())
		{
			dbContextOptions.EnableDetailedErrors();
			dbContextOptions.EnableSensitiveDataLogging();

			dbContextOptions.ConfigureWarnings(warningsConfiguration =>
			{
				warningsConfiguration.Log(
					CoreEventId.FirstWithoutOrderByAndFilterWarning,
					CoreEventId.RowLimitingOperationWithoutOrderByWarning);

				warningsConfiguration.Ignore(
					CoreEventId.SensitiveDataLoggingEnabledWarning);
			});
		}

		return dbContextOptions;
	}

	private static string GetSqlServerConnectionString(this IHostApplicationBuilder host) =>
		host.Configuration.GetConnectionString(ConfigurationKeys.SqlServer) ??
		throw new InvalidOperationException($"Could not find a connection string named '{ConfigurationKeys.SqlServer}'.");
}
