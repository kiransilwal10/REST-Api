namespace GameStore.Api.Endpoints;
using GameStore.Api.Dtos;

public static class GamesEndPoints
{

    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
       new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateOnly(2017, 3, 3)),
    new GameDto(2, "Super Mario Odyssey", "Platformer", 59.99m, new DateOnly(2017, 10, 27)),
    new GameDto(3, "Hollow Knight", "Metroidvania", 14.99m, new DateOnly(2017, 2, 24)),
    new GameDto(4, "Celeste", "Platformer", 19.99m, new DateOnly(2018, 1, 25)),
    new GameDto(5, "Stardew Valley", "Simulation", 14.99m, new DateOnly(2016, 2, 26))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");


        //GET /games
        group.MapGet("/", () =>
        {
            return Results.Ok(games);
        })
        .WithName("GetAllGames")
        .Produces<List<GameDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);


        //GET /games
        group.MapGet("/", () => games);

        //GET /games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);




        //POST /games

        group.MapPost("/", (CreateGameDto newGame) =>
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

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }
            var updated = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            games[index] = updated;
            return Results.Ok(updated);

        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            Results.NoContent();

        });
        return group;
    }
}