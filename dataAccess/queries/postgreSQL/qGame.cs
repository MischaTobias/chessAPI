namespace chessAPI.dataAccess.queries.postgreSQL;

public sealed class qGame : IQGame
{
    private const string _selectAll = @"
    SELECT id, player1Id, player2Id
    FROM public.game";
    private const string _selectOne = @"";
    private const string _add = @"
    INSERT INTO public.game(player1Id, player2Id)
    VALUES (@PLAYER1ID, @PLAYER2ID) returning id";
    private const string _delete = @"";
    private const string _update = @"";

    public string SQLGetAll => _selectAll;

    public string SQLDataEntity => _selectOne;

    public string NewDataEntity => _add;

    public string DeleteDataEntity => _delete;

    public string UpdateWholeEntity => _update;
}