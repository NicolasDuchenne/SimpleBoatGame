using System.Numerics;
using Raylib_cs;
public class MovingObstacles
{
    public static Dictionary<string, object> BarilExplosive = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/BarilExplosive.png")},
        { "explosive", true}

    };

    public static Dictionary<string, object> Baril = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/Baril.png")},
        { "explosive", false}
    };

    public static void Create(Dictionary<string, object> config, int col , int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new MovingObstacle(sprite, col, row, direction, canBeSentInThePast, (bool)config["explosive"]);
    }

}

public class MovingObstacle: DestroyableObstacle
{
    bool explosive ;

    public MovingObstacle(Sprite sprite, int column, int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true, bool explosive=false): base(sprite, column,  row, direction, canBeSentInThePast, explosive)
    {
        name+="Moving";
        CanBeMoved = true;
        CanMoveEntities = true;
    }
    
    public override void Destroy()
    {
        base.Destroy();
        if (Destroyed & explosive)
        {
            for (int column=-1; column<=1; column++)
            {
                for (int row = -1; row<=1; row++)
                {
                    if(0<=Column+column & Column+column< GameState.Instance.GridMap.ColumnNumber & 0<=Row+row&Row + row < GameState.Instance.GridMap.RowNumber)
                    {
                        
                        Vector2 shootingDirection = new Vector2(Column+column, Row+row)-new Vector2(Column, Row);
                        shootExplosive(shootingDirection);
                    }
                }
            }
        }
    }

    private void shootExplosive(Vector2 direction)
    {
        if (directions.Contains(direction))
        {
            Explosion explosion =Explosions.Create(Explosions.Baril, Column, Row, direction);
            explosion.Position = Position;
            explosion.Move(direction, false);
        }
    }
}



