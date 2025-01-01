using System.Numerics;
using Raylib_cs;

public class Mouse
{
    public Vector2 MousePos = new Vector2();

    public void Update()
    {
        MousePos = Raylib.GetMousePosition();
        MousePos.X -= GameState.Instance.XOffset;
        MousePos.X /= GameState.Instance.Scale;
        MousePos.Y -= GameState.Instance.YOffset;
        MousePos.Y /= GameState.Instance.Scale;
    }

}