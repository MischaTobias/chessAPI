using chessAPI.dataAccess.common;
using chessAPI.dataAccess.interfaces;
using chessAPI.dataAccess.models;
using chessAPI.dataAccess.repositores.game;
using chessAPI.models.game;
using chessAPI.models.player;
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
        p.Add("PLAYER1ID", newGame.player1Id);
        p.Add("PLAYER2ID", newGame.player2Id);
        return await add<TI>(p).ConfigureAwait(false);
    }

    protected override DynamicParameters fieldsAsParams(clsGameEntityModel<TI, TC> entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        var p = new DynamicParameters();
        p.Add("ID", entity.id);
        p.Add("PLAYER1ID", entity.player1Id);
        p.Add("PLAYER2ID", entity.player2Id);
        return p;
    }

    protected override DynamicParameters keyAsParams(TI key)
    {
        var p = new DynamicParameters();
        p.Add("ID", key);
        return p;
    }
}