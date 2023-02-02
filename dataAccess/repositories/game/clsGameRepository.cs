using chessAPI.dataAccess.common;
using chessAPI.dataAccess.interfaces;
using chessAPI.dataAccess.models;
using chessAPI.dataAccess.repositores.game;
using chessAPI.models.game;
using Dapper;

namespace chessAPI.dataAccess.repositores.player;

public sealed class clsGameRepository<TI, TC> : clsDataAccess<clsGameEntityModel<TI, TC>, TI, TC>, IGameRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    public clsGameRepository(IRelationalContext<TC> rkm,
                               ISQLData queries,
                               ILogger<clsGameRepository<TI, TC>> logger) : base(rkm, queries, logger)
    {
    }

    public async Task<TI> addGame(clsNewGame newGame)
    {
        var p = new DynamicParameters();
        p.Add("STARTED", newGame.started);
        p.Add("WHITES", newGame.whites);
        p.Add("BLACKS", newGame.blacks);
        p.Add("TURN", newGame.turn);
        p.Add("WINNER", newGame.winner);
        return await add<TI>(p).ConfigureAwait(false);
    }

    protected override DynamicParameters fieldsAsParams(clsGameEntityModel<TI, TC> entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        var p = new DynamicParameters();
        p.Add("ID", entity.id);
        p.Add("STARTED", entity.started);
        p.Add("WHITES", entity.whites);
        p.Add("BLACKS", entity.blacks);
        p.Add("TURN", entity.turn);
        p.Add("WINNER", entity.winner);
        return p;
    }

    protected override DynamicParameters keyAsParams(TI key)
    {
        var p = new DynamicParameters();
        p.Add("ID", key);
        return p;
    }
}