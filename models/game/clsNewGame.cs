namespace chessAPI.models.game;

public sealed class clsNewGame
{
    public clsNewGame(int whites, int blacks, int winner)
    {
        this.whites = whites;
        this.blacks = blacks;
        this.winner = winner;
    }

    public DateTime started { get; set; } = DateTime.Now;
    public int whites { get; set; }
    public int blacks { get; set; }
    public bool turn { get; set; } = true;
    public int winner { get; set; }
}