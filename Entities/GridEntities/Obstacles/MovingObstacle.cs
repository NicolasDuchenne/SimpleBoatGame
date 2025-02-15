using System.Numerics;
using Raylib_cs;
public class MovingObstacles
{
    private static Texture2D barilExplosiveTexture =  Raylib.LoadTexture("ressources/images/png/BarilExplosive.png");
    private static Texture2D barilTexture =  Raylib.LoadTexture("ressources/images/png/Baril.png");
    public static Dictionary<string, object> BarilExplosive = new Dictionary<string, object>
    {
        { "texture", barilExplosiveTexture},
        { "shader", DistortionShader.shader},
        { "explosive", true}

    };

    public static Dictionary<string, object> Baril = new Dictionary<string, object>
    {
        { "texture", barilTexture},
        { "shader", DistortionShader.shader},
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
    public MovingObstacle(Sprite sprite, int column, int row, Vector2 direction=new Vector2(), bool canBeSentInThePast=true, bool explosive=false): base(sprite, column,  row, direction, canBeSentInThePast, explosive)
    {
        name+="Moving";
        CanBeMoved = true;
        CanMoveEntities = true;
    }
}



