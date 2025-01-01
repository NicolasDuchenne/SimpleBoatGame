using System.Numerics;
using Raylib_cs;

public class MovableGridEntity: GridEntity
{
    public MovableGridEntity(Sprite sprite, int column, int row): base(sprite, column,  row)
    {
        CanMove = true;
        CanMoveEntities = true;
        // l'ennemie qui apparait ne peut pas être detruit touché lorsqu'il est créé car canBeHurt passe a true après son instantiation
        CanBeHurt = true;
    }
}