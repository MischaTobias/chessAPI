namespace chessAPI.models.game;

public sealed class clsGameOpponent<TI>
    where TI : struct, IEquatable<TI>
{
    public clsGameOpponent(TI id, TI blacks)
    {
        this.id = id;
        this.blacks = blacks;
    }
    public TI id { get; set; }
    public TI blacks { get; set; }
}