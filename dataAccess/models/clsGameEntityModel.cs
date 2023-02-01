using chessAPI.dataAccess.common;
namespace chessAPI.dataAccess.models;

public sealed class clsGameEntityModel<TI, TC> : relationalEntity<TI, TC>
        where TC : struct
        where TI : struct, IEquatable<TI>
{
    public clsGameEntityModel() { }

    public TI id { get; set; }
    public TI player1Id { get; set; }
    public TI player2Id { get; set; }
    public override TI key { get => id; set => id = value; }
}