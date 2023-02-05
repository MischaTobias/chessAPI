namespace chessAPI.models.game;

public sealed class clsNewGame<TI>
    where TI : struct, IEquatable<TI>
{
    public clsNewGame(TI whites)
    {
        this.whites = whites;
    }

    public TI whites { get; set; }
}