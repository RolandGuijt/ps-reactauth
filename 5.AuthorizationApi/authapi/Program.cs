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
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/user/settings", [Authorize] (UserRepository repo, ClaimsPrincipal user) =>
{
    var sub = user.FindFirstValue(ClaimTypes.NameIdentifier);
    if (sub is null)
        return [];
    return repo.GetSettings(sub);
});

app.UseAuthentication();
app.UseAuthorization();

app.Run();
