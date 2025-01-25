using System.Numerics;
using Raylib_cs;
public class Projectiles
{
    private static Texture2D missileTexture = Raylib.LoadTexture("ressources/images/png/Missile16.png");
    public static Dictionary<string, object> Missile = new Dictionary<string, object>
    {
        { "texture", missileTexture},
    };

    public static ProjectileGridEntity Create(Dictionary<string, object> config, int col , int row, Vector2 direction = new Vector2(), bool canBeSentInThePast=true)
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        return new ProjectileGridEntity(sprite, col, row, direction, canBeSentInThePast);
    }
    

    

}



