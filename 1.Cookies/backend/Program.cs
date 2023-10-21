using Globomantics.Backend.Repositories;
using Globomantics.Backend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaYarp();
builder.Services.AddSingleton<HouseRepository>();
builder.Services.AddSingleton<BidRepository>();
builder.Services.AddSingleton<UserRepository>();

builder.Services.AddAuthentication()
    .AddCookie(o =>
    {
        o.Cookie.Name = "__Host-spa";
        o.Cookie.SameSite = SameSiteMode.Strict;
        o.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });

var app = builder.Build();

app.MapGet("/houses", (HouseRepository repo) => repo.GetAll());
app.MapPost("/houses", (House house, HouseRepository repo) => 
{
  repo.Add(house);
  return Results.Created($"/houses/{house.Id}", house);
});

app.MapGet("/houses/{id:int}/bids", (int id, BidRepository repo) => repo.GetBids(id));
app.MapPost("houses/{id:int}/bids", (Bid bid, BidRepository repo) => repo.Add(bid));

app.UseSpaYarp();    
app.MapFallbackToFile("index.html");
app.Run();
