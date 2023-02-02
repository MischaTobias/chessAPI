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

    public async Task<clsGame<TI>> addGame(clsNewGame newGame)
    {
        var id = await gameRepository.addGame(newGame).ConfigureAwait(false);
        return new clsGame<TI>(id);
    }
}