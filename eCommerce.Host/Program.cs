using eCommerce.Application.DepenencyInjection;
using eCommerce.Infrastructure.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Console()
    .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
Log.Logger.Information("App is building..");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureSevice(builder.Configuration);
builder.Services.AddApplicationService();
builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("https://localhost:7077")
        .AllowCredentials();
    });
}
);
try
{
    var app = builder.Build();
    app.UseCors();
    app.UseSerilogRequestLogging();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseInfrastructureService();
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    Log.Logger.Information("App is running");

    app.Run();
}
catch(Exception ex)
{
    Log.Logger.Error(ex, "App failed to run");
}
finally
{
    Log.CloseAndFlush();
}