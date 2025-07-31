#region
using Restaurant.API.Middlewares;
using Restaurant.Application.Extensions;
using Restaurant.Infastructure.Extensions;
using Serilog;
#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Configure DbContext with SQL Server
builder.Services.AddInfastructure(builder.Configuration);
#endregion

#region Service Registeration
builder.Services.AddApplication();
#endregion

#region Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
#endregion

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();
app.UseMiddleware<RateLimitingMiddleware>();
app.UseSerilogRequestLogging();

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