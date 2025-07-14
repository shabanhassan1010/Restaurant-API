#region
using Restaurant.Application.IService;
using Restaurant.Application.Service;
using Restaurant.Domain.IRepository;
using Restaurant.Infastructure.Data.RepoImplementations;
using Restaurant.Infastructure.Extensions;
#endregion


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configure DbContext with SQL Server
builder.Services.AddInfastructure(builder.Configuration);
#endregion

#region Service Registeration
builder.Services.AddScoped<IResturantService, ResturantService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
