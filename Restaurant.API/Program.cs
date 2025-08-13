#region
using Microsoft.OpenApi.Models;
using Restaurant.API.Extensions;
using Restaurant.API.Middlewares;
using Restaurant.Application.EmailSettings;
using Restaurant.Application.Extensions;
using Restaurant.Domain.Entities;
using Restaurant.Infastructure.Extensions;
using Serilog;
#endregion
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting"));

// Configure Application
builder.AddPresentation();

// Configure DbContext with SQL Server
builder.Services.AddInfrastructure(builder.Configuration);

// Service Registeration
builder.Services.AddApplication();

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

//app.MapGroup("api/Identity").MapIdentityApi<User>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();