using Autofac;
using Autofac.Extensions.DependencyInjection;
using chessAPI;
using chessAPI.business.interfaces;
using chessAPI.models.game;
using chessAPI.models.player;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Serilog.Events;

//Serilog logger (https://github.com/serilog/serilog-aspnetcore)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("chessAPI starting");
    var builder = WebApplication.CreateBuilder(args);

    var connectionStrings = new connectionStrings();
    builder.Services.AddOptions();
    builder.Services.Configure<connectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
    builder.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

    // Two-stage initialization (https://github.com/serilog/serilog-aspnetcore)
    builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom
             .Configuration(context.Configuration)
             .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning).ReadFrom
             .Services(services).Enrich
             .FromLogContext().WriteTo
             .Console());

    // Autofac como inyección de dependencias
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new chessAPI.dependencyInjection<int, int>()));
    var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.UseMiddleware(typeof(chessAPI.customMiddleware<int>));
    app.MapGet("/", () =>
    {
        return "hola mundo";
    });

    app.MapPost("player",
    [AllowAnonymous] async (IPlayerBusiness<int> bs, clsNewPlayer newPlayer) =>
    {
        var result = await bs.addPlayer(newPlayer);
        if (result.id == 0) return Results.Problem("Email already exists.");
        return Results.Ok(result);
    });

    app.MapPut("player",
    [AllowAnonymous] async (IPlayerBusiness<int> bs, clsPlayer<int> player) => Results.Ok(await bs.updatePlayer(player)));

    app.MapGet("player/{playerId}",
    [AllowAnonymous] async (IPlayerBusiness<int> bs, int playerId) =>
    {
        var result = await bs.getPlayerById(playerId);
        if (result != null) return Results.Ok(result);
        return Results.NotFound(new errorMessage("Player not found."));
    });

    app.MapPost("game",
    [AllowAnonymous] async (IGameBusiness<int> bs, clsNewGame<int> newGame) => Results.Ok(await bs.addGame(newGame)));

    app.MapPut("game/addOpponent",
    [AllowAnonymous] async (IGameBusiness<int> bs, clsGameOpponent<int> gameOpponent) =>
    {
        try
        {
            var result = await bs.addOpponent(gameOpponent);
            if (result != null) return Results.Ok(result);
            return Results.BadRequest(new errorMessage("Players cannot belong to both teams involved a game."));
        }
        catch (Exception)
        {

        }
        return Results.BadRequest(new errorMessage("An error occured while trying to update the game."));
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "chessAPI terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
