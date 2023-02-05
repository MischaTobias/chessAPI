using chessAPI.models.game;

namespace chessAPI.business.interfaces;

public interface IGameBusiness<TI>
    where TI : struct, IEquatable<TI>
{
    Task<clsGame<TI>> addGame(clsNewGame<TI> newGame);
    Task<clsGame<TI>?> addOpponent(clsGameOpponent<TI> player);
}