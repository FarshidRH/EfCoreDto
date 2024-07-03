using Serilog;
using SerilogTracing;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateBootstrapLogger();

using var listener = new ActivityListenerConfiguration()
	.Instrument.AspNetCoreRequests()
	.TraceToSharedLogger();

Log.Information("Server starting");

try
{
	var builder = WebApplication.CreateBuilder(args);
	builder.AddWebApiServices();

	var app = builder.Build();
	app.ConfigureMiddlewares();

	await app.RunAsync();
	return 0;
}
catch (Exception exception)
{
	Log.Fatal(exception, "Server terminated unexpectedly");
	return 1;
}
finally
{
	await Log.CloseAndFlushAsync();
}
