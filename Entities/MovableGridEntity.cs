using System.Numerics;
using Raylib_cs;

public class MovableGridEntity: GridEntity
{
    public MovableGridEntity(Sprite sprite, int column, int row): base(sprite, column,  row)
    {
        CanMove = true;
        CanMoveEntities = true;
    }
}