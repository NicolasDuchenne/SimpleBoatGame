using System.Numerics;
using Raylib_cs;

public class EnemyGridEntity: GridEntity
{
    float timer = 0;
    
    public int TargetColumn {get; protected set;} = GameState.Instance.GridMap.Tiles.Count - 1;
    public int TargetRow {get; protected set;} = GameState.Instance.GridMap.Tiles[0].Count - 1;
    public Vector2 Direction = new Vector2(0,1);

    public EnemyGridEntity(Sprite sprite, int column, int row, Vector2 direction = new Vector2()): base(sprite, column,  row)
    {
        CanMove = false;
        CanMoveEntities = false;
        CanBeHurt = true;
        if (direction != new Vector2())
        {
            Direction = direction;
        }
        GameState.Instance.enemyNumber ++;
    }

    public override void Update()
    {
        GameState.Instance.debugMagic.AddOption("target column", TargetColumn);
        
        if (GameState.Instance.EnemyPlayTurn)
        {
            bool hasMoved = Move(Direction);
            if ((hasMoved == false) & (InThePast == false))
            {
                Direction.X = -Direction.X;
                Direction.Y = -Direction.Y;
                Move(Direction);
            }
            
        }
            
        base.Update();
    }

    public override void Destroy()
    {
        GameState.Instance.enemyNumber --;
        Destroyed = true;
    }

}