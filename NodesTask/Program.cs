using Microsoft.EntityFrameworkCore;
using NodesTask.Data;
using NodesTask.Helpers;
using NodesTask.Interfaces;
using NodesTask.Middleware;
using NodesTask.Services;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(LoggerHelper.GetLogFilePath(), rollingInterval: RollingInterval.Day)
    .CreateLogger();

var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
var connectionString = builder.Configuration.GetConnectionString(nameof(NodesApplicationDbContext));

builder.Services.AddDbContext<NodesApplicationDbContext>(config =>
{
    config.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
});

//builder.Services.AddScoped(typeof(ISqlQueryBuilderService<>), typeof(SqlQueryBuilderService<>));
builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
