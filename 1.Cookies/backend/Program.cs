using Globomantics.Backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaYarp();
builder.Services.AddSingleton<HouseRepository>();
builder.Services.AddSingleton<BidRepository>();

var app = builder.Build();

app.MapGet("/getallhouses", (HouseRepository repo) =>
{
    return repo.GetAll();
}).WithName("GetAllHouses");

app.MapGet("/getbids/{id:int}", (int id, BidRepository repo) =>
{
    return repo.GetBids(id);
}).WithName("GetBidByHouse");

if (app.Environment.IsDevelopment())
    app.UseSpaYarp();
    
app.MapFallbackToFile("index.html");

app.Run();
