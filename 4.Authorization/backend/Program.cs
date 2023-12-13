using System.Security.Claims;
using Globomantics.Backend.Models;
using Globomantics.Backend.Repositories;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaYarp();
builder.Services.AddSingleton<HouseRepository>();
builder.Services.AddSingleton<BidRepository>();
builder.Services.AddSingleton<UserRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication()
    .AddCookie(o =>
    {
        //Prevents overwrites from subdomains
        o.Cookie.Name = "__Host-spa";
        o.Cookie.SameSite = SameSiteMode.Strict;
        o.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = 
                StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });

var app = builder.Build();

app.MapGet("/houses", [Authorize] (HouseRepository repo) => repo.GetAll());
app.MapPost("/houses", [Authorize] (House house, HouseRepository repo) =>
{
    repo.Add(house);
    return Results.Created($"/houses/{house.Id}", house);
});

app.MapGet("/houses/{id:int}/bids", [Authorize] (int id, BidRepository repo) => repo.GetBids(id));
app.MapPost("houses/{id:int}/bids", [Authorize] (Bid bid, BidRepository repo) =>
{
    repo.Add(bid);
    return Results.Created($"/houses/{bid.HouseId}/bids", bid);
});

app.MapGet("/user/authzdata", [Authorize] (UserRepository repo, ClaimsPrincipal user) =>
{
    var sub = user.FindFirstValue("sub");
    if (sub is null)
        return [];
    return repo.GetAuthzData(sub);
});

app.MapControllers();

app.UseSpaYarp();

app.Run();
