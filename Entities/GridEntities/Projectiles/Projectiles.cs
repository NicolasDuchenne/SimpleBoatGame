using System.Numerics;
using Raylib_cs;
public class Projectiles
{
    public static Dictionary<string, object> Missile = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/Missile.png")},
    };

    public static void Create(Dictionary<string, object> config, int col , int row, Vector2 direction = new Vector2(), bool canBeSentInThePast=true)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        new ProjectileGridEntity(sprite, col, row, direction, canBeSentInThePast);
    }

}



