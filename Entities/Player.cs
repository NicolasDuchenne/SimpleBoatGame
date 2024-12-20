using System.Numerics;
using Raylib_cs;

public class Player: GridEntity
{
    public Player(Texture2D texture, int column, int row): base(texture, column,  row)
    {
        
    }
    public override void Update()
    {
        Vector2 direction = new Vector2();
        
        if (Raylib.IsKeyPressed(KeyboardKey.Right))
        {
            direction.X = 1;
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Left))
        {
            direction.X = -1;
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Up))
        {
            direction.Y = -1;
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Down))
        {
            direction.Y = 1;
        }
        if (direction != new Vector2())
        {
            Move(direction);
        }
            
        base.Update();
    }
}