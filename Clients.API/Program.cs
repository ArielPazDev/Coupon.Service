using Clients.API.Contexts;
using Clients.API.Interfaces;
using Clients.API.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<ClientAPIInterface, ClientAPIService>();

// Database
builder.Services.AddDbContext<DatabaseContext>
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Log
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(evt => evt.Level == Serilog.Events.LogEventLevel.Error)
    .WriteTo.File("Logs/Error-.log", rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(evt => evt.Level == Serilog.Events.LogEventLevel.Information)
    .WriteTo.File("Logs/Information-.log", rollingInterval: RollingInterval.Day))
    .CreateLogger();

// Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
