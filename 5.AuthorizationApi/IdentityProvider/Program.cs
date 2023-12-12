using Globomantics.Idp;
using Globomantics.Idp.Pages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var isBuilder = builder.Services.AddIdentityServer()
    .AddTestUsers(TestUsers.Users);

// in-memory, code config
isBuilder.AddInMemoryIdentityResources(Config.IdentityResources);
isBuilder.AddInMemoryApiResources(Config.ApiResources);
isBuilder.AddInMemoryApiScopes(Config.ApiScopes);
isBuilder.AddInMemoryClients(Config.Clients);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages()
    .RequireAuthorization();

app.Run();