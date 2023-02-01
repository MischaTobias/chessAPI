using chessAPI.models.player;

namespace chessAPI.business.interfaces;

public interface IGameBusiness<TI>
    where TI : struct, IEquatable<TI>
{

}