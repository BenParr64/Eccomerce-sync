using AutoMapper;
using Microsoft.AspNetCore.Authentication.Negotiate;
using OrderProcessingApi.Mappers;
using OrderProcessingApi.Services.ApiServices;
using OrderProcessingApi.Services.ApiServices.Interfaces;
using OrderProcessingApi.Services.Inventory;
using OrderProcessingApi.Services.Inventory.Interfaces;

var builder = WebApplication.CreateBuilder(args);


//Inventory Fetchers
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IWooInventoryFetcher, WooInventoryFetcher>();

// Add services to the container.
builder.Services.AddScoped<IInventoryService, InventoryService>();

//Api Services
builder.Services.AddScoped<IFetchWooApiService, FetchWooApiService>();

//Add Mappers
builder.Services.AddSingleton(new MapperConfiguration(mc => { mc.AddProfile(new WooItemProfile()); }).CreateMapper());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();