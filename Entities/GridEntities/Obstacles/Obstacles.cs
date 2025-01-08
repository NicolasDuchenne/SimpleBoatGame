using System.Numerics;
using Raylib_cs;
public class Obstacles
{
    public static Dictionary<string, object> Obstacle = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/Obstacle.png")},
    };

    public static void Create(Dictionary<string, object> config, int col , int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new Obstacle(sprite, col, row, direction, canBeSentInThePast);
    }

}

public class DestroyableObstacles
{
    public static Dictionary<string, object> Obstacle = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/Piques.png")},
    };

    public static void Create(Dictionary<string, object> config, int col , int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new DestroyableObstacle(sprite, col, row, direction, canBeSentInThePast);
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
    public DestroyableObstacle(Sprite sprite, int column, int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true): base(sprite, column,  row, direction, canBeSentInThePast)
    {
        name += "destroyableObstacle";
        CanBeHurt = true;
    }
}



