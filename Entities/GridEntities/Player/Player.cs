using System.Numerics;
using Raylib_cs;

public class Player
{
    public static Dictionary<string, object> Boat = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("images/png/Boat.png")},
    };

    public static void Create(int col , int row)
    {
        Sprite sprite = Sprite.SpriteFromConfig(Boat);
        new PlayerBoat(sprite, col, row);
    }
}

public class PlayerBoat: GridEntity
{
    Vector2 direction = new Vector2();
    public PlayerBoat(Sprite sprite, int column, int row, bool canBeSentInThePast = true): base(sprite, column,  row, canBeSentInThePast)
    {
        CanMoveEntities = true;
        CanBeHurt = true;
    }
    public override void Update()
    {
        
        if (Raylib.IsKeyPressed(KeyboardKey.Right))
        {
            direction = new Vector2(1, 0);
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Left))
        {
            direction = new Vector2(-1, 0);
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Up))
        {
            direction = new Vector2(0, -1);
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Down))
        {
            direction = new Vector2(0, 1);
        }
        if ((Timers.Instance.PlayerPlayTurn) & (direction != new Vector2()))
        {
            Move(direction);
            direction = new Vector2();
        }
            
        base.Update();
    }

    public override void Hit()
    {
        if (InThePast == false)
        {
            GameState.Instance.playerDead = true;
            Destroy();
        }
    }
}