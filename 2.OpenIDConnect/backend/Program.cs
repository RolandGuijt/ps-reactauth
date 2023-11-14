using Globomantics.Backend.Models;
using Globomantics.Backend.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaYarp();
builder.Services.AddAuthorization();
builder.Services.AddSingleton<HouseRepository>();
builder.Services.AddSingleton<BidRepository>();

builder.Services.AddBff(o => o.ManagementBasePath = "/account")
    .AddServerSideSessions();

builder.Services.AddAuthentication(o =>
    {
        o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(o =>
    {
        o.Cookie.Name = "__Host-spa";
        o.Cookie.SameSite = SameSiteMode.Strict;
        o.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    })
    .AddOpenIdConnect(options =>
    {
        options.Authority = "https://localhost:5001";

        options.ClientId = "react";
        //Store in application secrets
        options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
        options.ResponseType = "code";
    });

var app = builder.Build();

app.UseBff();

app.MapGet("/houses", [Authorize](HouseRepository repo) => repo.GetAll());
app.MapPost("/houses", [Authorize](House house, HouseRepository repo) =>
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

app.UseAuthorization();

app.MapBffManagementEndpoints();
app.UseSpaYarp();

app.Run();