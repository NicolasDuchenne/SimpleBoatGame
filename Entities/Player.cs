using System.Numerics;
using Raylib_cs;

public class Player: GridEntity
{
    Vector2 direction = new Vector2();
    public Player(Sprite sprite, int column, int row): base(sprite, column,  row)
    {
        CanMoveEntities = true;
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
        if ((GameState.Instance.PlayerPlayTurn) & (direction != new Vector2()))
        {
            Move(direction);
            direction = new Vector2();
        }
            
        base.Update();
    }
}