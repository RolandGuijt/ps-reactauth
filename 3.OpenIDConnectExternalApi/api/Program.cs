using Globomantics.Api.Models;
using Globomantics.Api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HouseRepository>();
builder.Services.AddSingleton<BidRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => o.Authority = "https://localhost:5001");

var app = builder.Build();

app.UseAuthentication();

app.MapGet("/houses", (HouseRepository repo) => repo.GetAll());
app.MapPost("/houses", (House house, HouseRepository repo) =>
{
    repo.Add(house);
    return Results.Created($"/houses/{house.Id}", house);
});

app.MapGet("/houses/{id:int}/bids", (int id, BidRepository repo) => repo.GetBids(id));
app.MapPost("houses/{id:int}/bids", (Bid bid, BidRepository repo) =>
{
    repo.Add(bid);
    return Results.Created($"/houses/{bid.HouseId}/bids", bid);
});

app.Run();
