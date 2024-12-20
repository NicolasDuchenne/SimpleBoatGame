using System.Numerics;
using Raylib_cs;

public class MovableGridEntity: GridEntity
{
    public MovableGridEntity(Texture2D texture, int column, int row): base(texture, column,  row)
    {
        CanMove = true;
        CanMoveEntities = true;
    }
}