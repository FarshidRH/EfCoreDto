var builder = WebApplication.CreateBuilder(args);
builder.AddWebApiServices();

var app = builder.Build();
app.ConfigureMiddlewares();
await app.RunAsync();
