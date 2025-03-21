using simpleAPI;
using simpleAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

//DI mongoDB
builder.Services.AddSingleton<MongoDBContext>();
builder.Services.AddSingleton<MongoService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

