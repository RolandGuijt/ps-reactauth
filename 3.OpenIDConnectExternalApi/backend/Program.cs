using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaYarp();

builder.Services.AddBff(o => o.ManagementBasePath = "/account")
    .AddServerSideSessions()
    .AddRemoteApis();

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
        options.Scope.Add("globoapi");
        options.SaveTokens = true;
    });

var app = builder.Build();

app.UseBff();
app.MapBffManagementEndpoints();
app.MapRemoteBffApiEndpoint("/api", "https://localhost:7165")
    .RequireAccessToken();
app.UseSpaYarp();

app.Run();