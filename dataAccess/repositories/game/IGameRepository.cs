using chessAPI.models.game;

namespace chessAPI.dataAccess.repositores.game;

public interface IGameRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    Task<TI> addGame(clsNewGame newGame);
}