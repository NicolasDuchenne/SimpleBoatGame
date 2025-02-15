using System.Numerics;
using Raylib_cs;
public class Obstacles
{
    private static Texture2D obstacleTexture =  Raylib.LoadTexture("ressources/images/png/Obstacle.png");
    public static Dictionary<string, object> Obstacle = new Dictionary<string, object>
    {
        { "texture", obstacleTexture},
        { "shader", DistortionShader.shader},
    };

    public static void Create(Dictionary<string, object> config, int col , int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new Obstacle(sprite, col, row, direction, canBeSentInThePast);
    }

}

public class DestroyableObstacles
{
    private static Texture2D piquesTexture =  Raylib.LoadTexture("ressources/images/png/Piques.png");
    private static Texture2D explosivePiquesTexture =  Raylib.LoadTexture("ressources/images/png/ExplosivePiques.png");
    
    public static Dictionary<string, object> Obstacle = new Dictionary<string, object>
    {
        { "texture", piquesTexture},
        { "shader", DistortionShader.shader},
        { "explosive", false}
    };
    public static Dictionary<string, object> ExplosiveObstacle = new Dictionary<string, object>
    {
        { "texture", explosivePiquesTexture},
        { "shader", DistortionShader.shader},
        { "explosive", true}
    };

    public static void Create(Dictionary<string, object> config, int col , int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new DestroyableObstacle(sprite, col, row, direction, canBeSentInThePast, (bool)config["explosive"]);
    }

}

public class Obstacle: GridEntity
{
    public Obstacle(Sprite sprite, int column, int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true): base(sprite, column,  row, direction, canBeSentInThePast)
    {
        name += "obstacle";
        CanBeHurt = false;
    }
}

public class DestroyableObstacle: GridEntity
{
    bool explosive;
    public DestroyableObstacle(Sprite sprite, int column, int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true, bool explosive=false): base(sprite, column,  row, direction, canBeSentInThePast)
    {
        name += "destroyableObstacle";
        CanBeHurt = true;
        this.explosive = explosive;
        if (explosive)
        {
            name+="Explosive";
        }
    }
    
    public override void Destroy()
    {
        base.Destroy();
        if (Destroyed & explosive)
        {
            for (int col=-1; col<=1; col++)
            {
                for (int row = -1; row<=1; row++)
                {
                    if(0<=Column+col & Column+col< GameState.Instance.GridMap.ColumnNumber & 0<=Row+row&Row + row < GameState.Instance.GridMap.RowNumber)
                    {
                        
                        Vector2 shootingDirection = new Vector2(Column+col, Row+row)-new Vector2(Column, Row);
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



