using System.Numerics;
using Raylib_cs;
public class ProjectileGridEntity: GridEntity
{
    public ProjectileGridEntity(Sprite sprite, int column, int row, Vector2 direction = new Vector2(), bool canBeSentInThePast=true): base(sprite, column,  row, canBeSentInThePast)
    {
        CanBeMoved = false;
        CanBeMovedEntities = false;
        CanBeHurt = false;
        CanHurt = true;
        Rotation = 90;
    }
    public override void Update()
    {
        base.Update();
    }
}