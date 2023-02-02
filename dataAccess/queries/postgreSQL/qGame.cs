namespace chessAPI.dataAccess.queries.postgreSQL;

public sealed class qGame : IQGame
{
    private const string _selectAll = @"
    SELECT id, started, whites, blacks, turn, winner
    FROM public.game";
    private const string _selectOne = @"";
    private const string _add = @"
    INSERT INTO public.game(started, whites, blacks, turn, winner)
    VALUES (@STARTED, @WHITES, @BLACKS, @TURN, @WINNER) returning id";
    private const string _delete = @"";
    private const string _update = @"";

    public string SQLGetAll => _selectAll;

    public string SQLDataEntity => _selectOne;

    public string NewDataEntity => _add;

    public string DeleteDataEntity => _delete;

    public string UpdateWholeEntity => _update;
}