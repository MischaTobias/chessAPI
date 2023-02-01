namespace chessAPI.models.game;

public sealed class clsGame<TI>
    where TI : struct, IEquatable<TI>
{
    public clsGame(TI id, int player1Id, int player2Id)
    {
        this.id = id;
        this.player1Id = player1Id;
        this.player2Id = player2Id;
    }

    public TI id { get; set; }
    public int player1Id { get; set; }
    public int player2Id { get; set; }
}