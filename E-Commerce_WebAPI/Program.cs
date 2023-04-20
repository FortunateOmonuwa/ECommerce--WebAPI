using Data;
using DataAccess.Repository;
using Domain.Contract;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Service.IServiceContracts;
using Service.OrderContract;
using Service.ServiceRepository;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IBaseContract<Review>, ReviewRepository>();

builder.Services.AddScoped<IBaseContract<Product>, ProductRepository>();
builder.Services.AddScoped<IBaseContract2<Review>, ProductRepository>();

builder.Services.AddScoped<IBaseContract<Category>, CategoryRepository>();

//builder.Services.AddScoped<IBaseContract<Order>, OrderRepository>();
//builder.Services.AddScoped<IBaseContract2<OrderDetails>, OrderRepository>();
builder.Services.AddScoped<IBaseContract<Customer>, CustomerRepository>();

builder.Services.AddScoped<ICartService, CartServiceRepository>();

builder.Services.AddScoped<IOrderService, OrderServiceRepository>();

builder.Services.AddScoped<IBaseContractNR<Review>, CustomerRepository>();

//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



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
