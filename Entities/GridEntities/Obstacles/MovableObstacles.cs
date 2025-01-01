using Raylib_cs;
public class MovableObstacles
{
    public static Dictionary<string, object> Baril = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("images/png/Baril.png")},
    };

    public static void Create(Dictionary<string, object> config, int col , int row)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new MovableGridEntity(sprite, col, row);
    }

}



