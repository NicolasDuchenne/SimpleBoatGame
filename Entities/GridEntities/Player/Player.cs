using System.Numerics;
using Raylib_cs;

public class Player
{
    public static Dictionary<string, object> Boat = new Dictionary<string, object>
    {
        { "texture", Raylib.LoadTexture("ressources/images/png/Boat.png")},
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
    Vector2 oldDirection = new Vector2();
    public PlayerBoat(Sprite sprite, int column, int row, Vector2 direction=new Vector2(), bool canBeSentInThePast = true): base(sprite, column,  row, direction, canBeSentInThePast)
    {
        name += "player";
        CanMoveEntities = true;
        CanBeHurt = true;
        CanBeMoved = false;
        IsPlayer = true;
    }

    public override void Update()
    {
        
        if ((Raylib.IsKeyDown(KeyboardKey.Right)) ||(Raylib.IsKeyDown(KeyboardKey.D)))
        {
            direction = new Vector2(1, 0);
        }
        else if ((Raylib.IsKeyDown(KeyboardKey.Left))||(Raylib.IsKeyDown(KeyboardKey.A)))
        {
            direction = new Vector2(-1, 0);
        }
        else if ((Raylib.IsKeyDown(KeyboardKey.Up))||(Raylib.IsKeyDown(KeyboardKey.W)))
        {
            direction = new Vector2(0, -1);
        }
        else if ((Raylib.IsKeyDown(KeyboardKey.Down))||(Raylib.IsKeyDown(KeyboardKey.S)))
        {
            direction = new Vector2(0, 1);
        }
        // This condition ensure that if we keep the key down a little bit to long, we do not move 2 times
        if (direction != new Vector2() & oldDirection!=new Vector2())
        {
            if (Timers.Instance.PlayerTimer<0.1)
            {
                direction = new Vector2();
            }
            else
            {
                oldDirection = new Vector2();
            }
            
        }

        if ((Timers.Instance.PlayerPlayTurn) & (direction != new Vector2()))
        {
            Move(direction);
            oldDirection=direction;
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