using ABBTask.Data.Data;
using ABBTask.Interfaces;
using ABBTask.Repositories;
using ABBTask.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEstateRepository, EstateRepository>();
builder.Services.AddScoped<IEstateService, EstateService>();
builder.Services.AddScoped<IEstateValidatorService, EstateValidatorService>();
builder.Services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<EstateRepository>>());
builder.Services.AddDbContext<ABBTaskDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
