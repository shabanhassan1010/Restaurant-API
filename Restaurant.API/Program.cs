#region
using Restaurant.Application.RestaurantService.ServicesExtensions;
using Restaurant.Infastructure.Extensions;
using Serilog;
using Serilog.Events;
#endregion


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configure DbContext with SQL Server
builder.Services.AddInfastructure(builder.Configuration);
#endregion

#region Service Registeration
builder.Services.AddApplication();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
