using System.Numerics;
using Raylib_cs;
public class Enemies
{
    private static Texture2D tortueTexture =  Raylib.LoadTexture("ressources/images/png/Tortue.png");
    private static Texture2D fregateTexture =  Raylib.LoadTexture("ressources/images/png/Fregate.png");
    private static Texture2D flamandTexture =  Raylib.LoadTexture("ressources/images/png/FlamandRose.png");
    private static Texture2D baleineTexture =  Raylib.LoadTexture("ressources/images/png/Baleine32.png");
    private static Texture2D pieuvreTexture =  Raylib.LoadTexture("ressources/images/png/Pieuvre.png");
    public static Dictionary<string, object> Tortue = new Dictionary<string, object>
    {
        { "texture", tortueTexture},
        { "shader", DistortionShader.shader},
        {"shootTurn", 3}
    };

    public static Dictionary<string, object> Fregate = new Dictionary<string, object>
    {
        { "texture", fregateTexture},
        { "shader", DistortionShader.shader},
        { "nCol", 1 },
        { "nRow", 6 },
        { "fps", 6 },
        { "width", 32 },
        { "height", 32 },
        {"shootTurn", 2}
    };

    public static Dictionary<string, object> FlamandRose = new Dictionary<string, object>
    {
        { "texture", flamandTexture},
        { "shader", DistortionShader.shader},
        { "nCol", 1 },
        { "nRow", 6 },
        { "fps", 6 },
        { "width", 32 },
        { "height", 32 },
        {"shootTurn", 2}
    };

    public static Dictionary<string, object> Baleine = new Dictionary<string, object>
    {
        { "texture", baleineTexture},
        { "shader", DistortionShader.shader},
        {"shootTurn", 1}
    };

    public static Dictionary<string, object> Pieuvre = new Dictionary<string, object>
    {
        { "texture", pieuvreTexture},
        { "shader", DistortionShader.shader},
        {"shootTurn", 0}
    };

    

    public static void Create(Dictionary<string, object> config, int col , int row, Vector2 direction = new Vector2(), bool canBeSentInThePast=true, int shootCounter = 0)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new EnemyGridEntity(sprite, col, row, direction, canBeSentInThePast, (int)config["shootTurn"], shootCounter);
    }

}



