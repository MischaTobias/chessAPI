using chessAPI.business.interfaces;
using chessAPI.dataAccess.repositores.player;
using chessAPI.models.player;

namespace chessAPI.business.impl;

public sealed class clsPlayerBusiness<TI, TC> : IPlayerBusiness<TI>
    where TI : struct, IEquatable<TI>
    where TC : struct
{
    internal readonly IPlayerRepository<TI, TC> playerRepository;

    public clsPlayerBusiness(IPlayerRepository<TI, TC> playerRepository)
    {
        this.playerRepository = playerRepository;
    }

    public async Task<clsPlayer<TI>> addPlayer(clsNewPlayer newPlayer)
    {
        var x = await playerRepository.addPlayer(newPlayer).ConfigureAwait(false);
        return new clsPlayer<TI>(x, newPlayer.email);
    }

    public async Task<clsPlayer<TI>?> getPlayerById(TI playerId)
    {
        var playerModel = await playerRepository.getPlayerById(playerId).ConfigureAwait(false);
        return playerModel == null ? null : new clsPlayer<TI>(playerModel.id, playerModel.email);
    }

    public async Task<clsPlayer<TI>?> updatePlayer(clsPlayer<TI> player)
    {
        var playerModel = await playerRepository.getPlayerById(player.id).ConfigureAwait(false);
        if (playerModel == null) return null;
        var result = await playerRepository.updatePlayer(player).ConfigureAwait(false);
        return result == null ? null : new clsPlayer<TI>(result.id, result.email);
    }
}