using chessAPI.dataAccess.models;
using chessAPI.models.game;

namespace chessAPI.dataAccess.repositores.game;

public interface IGameRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    Task<clsGameEntityModel<TI, TC>> addGame(clsNewGame<TI> newGame);
    Task<clsGameEntityModel<TI, TC>?> getGameById(TI id);
    Task<clsGameEntityModel<TI, TC>?> updateGame(clsGame<TI> game);

}