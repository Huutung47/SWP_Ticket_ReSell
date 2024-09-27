using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Swashbuckle.AspNetCore.Filters;
using SWP_Ticket_ReSell_DAO.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//--
builder.Services.AddDbContext<swp1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB"))
           .UseLazyLoadingProxies()
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors());

// add scope api
builder.Services.AddScoped(typeof(ServiceBase<>));
builder.Services.AddScoped(typeof(GenericRepository<>));
// 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// enable authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
