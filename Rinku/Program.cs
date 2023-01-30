using Microsoft.EntityFrameworkCore;
using Rinku.Models;
using Rinku.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RinkuContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(options =>
{
    options.WithOrigins("http://127.0.0.1:5500");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapMovimientosEndpoints();

app.Run();
