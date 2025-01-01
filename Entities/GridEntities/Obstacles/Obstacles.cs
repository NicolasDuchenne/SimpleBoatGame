using Raylib_cs;
public class Obstacles
{
    public static Dictionary<string, object> Obstacle = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("images/png/Obstacle.png")},
    };

    public static void Create(Dictionary<string, object> config, int col , int row)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new GridEntity(sprite, col, row);
    }

}



