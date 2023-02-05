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

    public async Task<clsGameEntityModel<TI, TC>> addGame(clsNewGame<TI> newGame)
    {
        var p = new DynamicParameters();
        p.Add("STARTED", DateTime.Now);
        p.Add("WHITES", newGame.whites);
        p.Add("TURN", true);
        return await add<clsGameEntityModel<TI, TC>>(p).ConfigureAwait(false);
    }

    public async Task<clsGameEntityModel<TI, TC>?> getGameById(TI id) => await getEntity(id).ConfigureAwait(false);

    public async Task<clsGameEntityModel<TI, TC>?> updateGame(clsGame<TI> game)
    {
        var p = new DynamicParameters();
        p.Add("ID", game.id);
        p.Add("STARTED", game.started);
        p.Add("WHITES", game.whites);
        p.Add("BLACKS", game.blacks);
        p.Add("TURN", game.turn);
        p.Add("WINNER", game.winner);
        return await set<clsGameEntityModel<TI, TC>>(p, null, queries.UpdateWholeEntity, null).ConfigureAwait(false);
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