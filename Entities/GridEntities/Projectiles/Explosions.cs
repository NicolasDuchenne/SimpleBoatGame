using System.Numerics;
using Raylib_cs;


public class Explosions
{
    public static Dictionary<string, object> Baril = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/Explosion.png")},
    };

    public static Explosion Create(Dictionary<string, object> config, int col , int row, Vector2 direction = new Vector2())
    {
        Sprite sprite = Sprite.SpriteFromConfig(config);
        return new Explosion(sprite, col, row, direction);
    }
}

public class Explosion: GridEntity
{
    private float explosionTimer = 0;
    private float explosionDuration = 0.5f;
    public Explosion(Sprite sprite, int column, int row, Vector2 direction = new Vector2(), bool canBeSentInThePast=true): base(sprite, column,  row, direction, canBeSentInThePast, false)
    {
        name += "explosion";
        CanBeMoved = false;
        CanMoveEntities = false;
        CanBeHurt = true;
        CanHurt = true;
        goesThroughEntities = true;
        speed = 250;
        updateSpeedWithGridSize();
    }
    public override void Update()
    {
        base.Update();
        explosionTimer+=Raylib.GetFrameTime();
        if (explosionTimer>explosionDuration)
        {
            GameState.Instance.GridMap.Tiles[Column][Row].setEntity(this);
            if (Destroyed == false)
            {
                Destroy();  
                GameState.Instance.GridMap.Tiles[Column][Row].removeEntity(name);
            }
            
        }
    }
}