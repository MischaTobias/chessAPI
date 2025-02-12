using chessAPI.dataAccess.common;
using chessAPI.dataAccess.interfaces;
using chessAPI.dataAccess.models;
using chessAPI.models.player;
using Dapper;

namespace chessAPI.dataAccess.repositores.player;

public sealed class clsPlayerRepository<TI, TC> : clsDataAccess<clsPlayerEntityModel<TI, TC>, TI, TC>, IPlayerRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    public clsPlayerRepository(IRelationalContext<TC> rkm,
                               ISQLData queries,
                               ILogger<clsPlayerRepository<TI, TC>> logger) : base(rkm, queries, logger)
    {
    }

    public async Task<TI> addPlayer(clsNewPlayer newPlayer)
    {
        var p = new DynamicParameters();
        p.Add("EMAIL", newPlayer.email);
        return await add<TI>(p).ConfigureAwait(false);
    }

    public async Task<IEnumerable<clsPlayerEntityModel<TI, TC>>> addPlayers(IEnumerable<clsNewPlayer> players)
    {
        var r = new List<clsPlayerEntityModel<TI, TC>>(players.Count());
        foreach (var player in players)
        {
            TI playerId = await addPlayer(player).ConfigureAwait(false);
            r.Add(new clsPlayerEntityModel<TI, TC>() { id = playerId, email = player.email });
        }
        return r;
    }

    public Task deletePlayer(TI id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<clsPlayerEntityModel<TI, TC>>> getPlayersByGame(TI gameId)
    {
        throw new NotImplementedException();
    }

    public async Task<clsPlayerEntityModel<TI, TC>?> getPlayerById(TI id) => await getEntity(id).ConfigureAwait(false);

    public async Task<clsPlayerEntityModel<TI, TC>?> updatePlayer(clsPlayer<TI> player)
    {
        var p = new DynamicParameters();
        p.Add("EMAIL", player.email);
        p.Add("ID", player.id);
        return await set<clsPlayerEntityModel<TI, TC>>(p, null, queries.UpdateWholeEntity, null).ConfigureAwait(false);
    }

    protected override DynamicParameters fieldsAsParams(clsPlayerEntityModel<TI, TC> entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        var p = new DynamicParameters();
        p.Add("ID", entity.id);
        p.Add("EMAIL", entity.email);
        return p;
    }

    protected override DynamicParameters keyAsParams(TI key)
    {
        var p = new DynamicParameters();
        p.Add("ID", key);
        return p;
    }
}