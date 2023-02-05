using chessAPI.business.interfaces;
using chessAPI.dataAccess.repositores.game;
using chessAPI.models.game;

namespace chessAPI.business.impl;

public sealed class clsGameBusiness<TI, TC> : IGameBusiness<TI>
    where TI : struct, IEquatable<TI>
    where TC : struct
{
    internal readonly IGameRepository<TI, TC> gameRepository;

    public clsGameBusiness(IGameRepository<TI, TC> gameRepository)
    {
        this.gameRepository = gameRepository;
    }

    public async Task<clsGame<TI>> addGame(clsNewGame<TI> newGame)
    {
        var game = await gameRepository.addGame(newGame).ConfigureAwait(false);
        return game.getModel();
    }

    public async Task<clsGame<TI>?> addOpponent(clsGameOpponent<TI> gameOpponent)
    {
        var gameModel = await gameRepository.getGameById(gameOpponent.id).ConfigureAwait(false);
        if (gameModel == null) return null;
        gameModel.blacks = gameOpponent.blacks;
        var game = gameModel.getModel();
        var result = await gameRepository.updateGame(game).ConfigureAwait(false);
        if (result == null) return null;
        return result.getModel();
    }
}