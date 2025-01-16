using System.Numerics;
using Raylib_cs;
public class Enemies
{
    public static Dictionary<string, object> Tortue = new Dictionary<string, object>
    {
        // { "texture", Raylib.LoadTexture("ressources/images/png/Fregate.png")},
        // { "nCol", 1 },
        // { "nRow", 6 },
        // { "fps", 6 },
        // { "width", 32 },
        // { "height", 32 },
        // {"shootTurn", 3}
        { "texture", Raylib.LoadTexture("ressources/images/png/Tortue.png")},
        {"shootTurn", 3}
    };

    public static Dictionary<string, object> Fregate = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/Fregate.png")},
        { "nCol", 1 },
        { "nRow", 6 },
        { "fps", 6 },
        { "width", 32 },
        { "height", 32 },
        {"shootTurn", 2}
    };

    public static Dictionary<string, object> FlamandRose = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/FlamandRose.png")},
        { "nCol", 1 },
        { "nRow", 6 },
        { "fps", 6 },
        { "width", 32 },
        { "height", 32 },
        {"shootTurn", 2}
    };

    public static Dictionary<string, object> Baleine = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/Baleine32.png")},
        {"shootTurn", 1}
    };

    public static Dictionary<string, object> Pieuvre = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/Pieuvre.png")},
        {"shootTurn", 0}
    };

    

    public static void Create(Dictionary<string, object> config, int col , int row, Vector2 direction = new Vector2(), bool canBeSentInThePast=true)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new EnemyGridEntity(sprite, col, row, direction, canBeSentInThePast, (int)config["shootTurn"]);
    }

}



