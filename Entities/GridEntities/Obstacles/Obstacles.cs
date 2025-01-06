using Raylib_cs;
public class Obstacles
{
    public static Dictionary<string, object> Obstacle = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/Obstacle.png")},
    };

    public static void Create(Dictionary<string, object> config, int col , int row, bool canBeSentInThePast=true)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new GridEntity(sprite, col, row, canBeSentInThePast);
    }

}



