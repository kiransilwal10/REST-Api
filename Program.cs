using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
    new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
    new GameDto(2, "Super Mario Odyssey", "Platformer", 59.99m, new DateOnly(2017, 10, 27)),
    new GameDto(3, "Hollow Knight", "Metroidvania", 14.99m, new DateOnly(2017, 2, 24)),
    new GameDto(4, "Celeste", "Platformer", 19.99m, new DateOnly(2018, 1, 25)),
    new GameDto(5, "Stardew Valley", "Simulation", 14.99m, new DateOnly(2016, 2, 26))
];
 
//GET /games
app.MapGet("/games",()=> games);

//GET /games/{id}
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);

//POST /games

app.MapPost("/games", (CreateGameDto newGame) =>
{

    GameDto game = new GameDto(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

//PUT/ games

app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(game => game.Id == id);

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );
    return Results.NoContent();

});
 


app.Run();
