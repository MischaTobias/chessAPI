namespace chessAPI.dataAccess.queries.postgreSQL;

public sealed class qGame : IQGame
{
    private const string _selectAll = @"
    SELECT id, started, whites, blacks, turn, winner
    FROM public.game";
    private const string _selectOne = @"
    SELECT id, started, whites, blacks, turn, winner
    FROM public.game
    WHERE id = @ID";
    private const string _add = @"
    INSERT INTO public.game(started, whites, turn)
    VALUES (@STARTED, @WHITES, @TURN)
    RETURNING id, started, whites, blacks, turn, winner";
    private const string _delete = @"";
    private const string _update = @"
    UPDATE public.game
    SET 
        started = @STARTED,
        whites = @WHITES,
        blacks = @BLACKS,
        turn = @TURN,
        winner = @WINNER
    WHERE id = @ID
    AND NOT EXISTS(
    SELECT a.player_id
    FROM team_player a
    WHERE team_id = @WHITES
    AND EXISTS (
                    SELECT 1
                    FROM team_player b
                    WHERE team_id = @BLACKS and a.player_id = b.player_id
                )
    )
    RETURNING id, started, whites, blacks, turn, winner";

    public string SQLGetAll => _selectAll;

    public string SQLDataEntity => _selectOne;

    public string NewDataEntity => _add;

    public string DeleteDataEntity => _delete;

    public string UpdateWholeEntity => _update;
}