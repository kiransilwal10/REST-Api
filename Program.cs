using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
    new GameDto(2, "Super Mario Odyssey", "Platformer", 59.99m, new DateOnly(2017, 10, 27)),
    new GameDto(3, "Hollow Knight", "Metroidvania", 14.99m, new DateOnly(2017, 2, 24)),
    new GameDto(4, "Celeste", "Platformer", 19.99m, new DateOnly(2018, 1, 25)),
    new GameDto(5, "Stardew Valley", "Simulation", 14.99m, new DateOnly(2016, 2, 26))
];

app.MapGet("games",()=> games);

app.MapGet("/", () => "Hello World!");

app.Run();
