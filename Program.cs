using GameStore.Api.Dtos;
using Microsoft.Extensions.Options;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndpoints();




app.Run();
