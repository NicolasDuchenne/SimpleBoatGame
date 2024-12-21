using System.Numerics;
using Raylib_cs;

public class EnemyGridEntity: GridEntity
{
    float timer = 0;
    public EnemyGridEntity(Sprite sprite, int column, int row): base(sprite, column,  row)
    {
        CanMove = false;
        CanMoveEntities = false;
    }

    public override void Update()
    {
        Vector2 direction = new Vector2();
        
        if (GameState.Instance.EnemyPlayTurn)
        {
            direction = new Vector2(1,0);
            Move(direction);
        }
            
        base.Update();
    }
}