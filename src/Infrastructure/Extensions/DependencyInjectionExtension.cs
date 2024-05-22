using EfCoreDto.Infrastructure.Services;
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

	private static DbContextOptionsBuilder ConfigureDbContextOptions(
		this DbContextOptionsBuilder dbContextOptions, IHostApplicationBuilder host)
	{
		var connectionString = host.Configuration.GetConnectionString("SqlServer");
		dbContextOptions.UseSqlServer(connectionString, sqlServerOptionsBuilder =>
		{
			sqlServerOptionsBuilder.EnableRetryOnFailure();
		});

		if (host.Environment.IsDevelopment())
		{
			dbContextOptions.EnableDetailedErrors();
			dbContextOptions.EnableSensitiveDataLogging();
			dbContextOptions.ConfigureWarnings(warningsConfigurationBuilder =>
			{
				warningsConfigurationBuilder.Log(
					CoreEventId.FirstWithoutOrderByAndFilterWarning,
					CoreEventId.RowLimitingOperationWithoutOrderByWarning);
			});
		}

		return dbContextOptions;
	}
}
