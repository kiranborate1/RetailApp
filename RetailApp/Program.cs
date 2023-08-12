using Microsoft.EntityFrameworkCore;
using Retail.Models;
using RetailApp.Entities;
using RetailApp.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);

string str = "Server=DESKTOP-1DAA8VK;Initial Catalog=Retail_App;Trusted_Connection=True;";
builder.Services.AddDbContext<RetailDbContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSqlServer(str);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRetailDbContext, RetailDbContext>();
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
