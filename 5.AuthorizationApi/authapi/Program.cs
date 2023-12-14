using Globomantics.Backend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => 
    { 
        o.Authority = "https://localhost:5001"; 
        o.Audience = "globoauthapi";
        o.MapInboundClaims = false;
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/user/authzdata/{applicationId}", [Authorize] (UserRepository repo, ClaimsPrincipal user, int applicationId) =>
{
    var sub = user.FindFirstValue("sub");
    if (sub is null)
        return [];
    return repo.GetAuthzData(applicationId, sub);
});

app.UseAuthentication();
app.UseAuthorization();

app.Run();
