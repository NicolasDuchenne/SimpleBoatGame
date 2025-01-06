using System.Numerics;
using Raylib_cs;
public class MovableObstacles
{
    public static Dictionary<string, object> Baril = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/Baril.png")},
    };

    public static void Create(Dictionary<string, object> config, int col , int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new MovableObstacle(sprite, col, row, direction, canBeSentInThePast);
    }

}

public class MovableObstacle: GridEntity
{
    public MovableObstacle(Sprite sprite, int column, int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true): base(sprite, column,  row, direction, canBeSentInThePast)
    {
        CanBeMoved = true;
        CanMoveEntities = true;
        CanBeHurt = true;
    }
}



