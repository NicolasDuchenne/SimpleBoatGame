using System.Numerics;
using Raylib_cs;
public class ProjectileGridEntity: GridEntity
{
    public ProjectileGridEntity(Sprite sprite, int column, int row, Vector2 direction = new Vector2(), bool canBeSentInThePast=true): base(sprite, column,  row, direction, canBeSentInThePast, false)
    {
        name += "projectile";
        CanBeMoved = false;
        CanMoveEntities = false;
        CanBeHurt = true;
        CanHurt = true;
    }
    public override void Update()
    {
        base.Update();
        if (Timers.Instance.HalfSecondTurn)
        {

            Move(Direction);
            if (positionWasClamped)
            {
                Destroy();
                GameState.Instance.GridMap.Tiles[Column][Row].removeEntity(name);    
            }
                
        }
    }
    protected override float GetAngleFromDirection(Vector2 direction)
    {
        
        if (direction == new Vector2(0, 1)) 
        {
            return 90f;  
        }
        if (direction == new Vector2(1, 0)) 
        {
            Flip = false;
            return 0; 
        }
        if (direction == new Vector2(0, -1))
        {
            return -90f; 
        } 
        if (direction == new Vector2(-1, 0))
        {
            Flip = true;
            return 0f;  
        } 
        return 0f; // Default
    }
}