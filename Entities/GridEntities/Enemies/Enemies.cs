using Raylib_cs;
public class Enemies
{
    public static Dictionary<string, object> Fregate = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("images/png/Fregate.png")},
        { "nCol", 1 },
        { "nRow", 6 },
        { "fps", 6 },
        { "width", 32 },
        { "height", 32 },
    };

    public static void Create(Dictionary<string, object> config, int col , int row)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new EnemyGridEntity(sprite, col, row);
    }

}



