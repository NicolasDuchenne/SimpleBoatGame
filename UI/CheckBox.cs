using System.Numerics;
using Raylib_cs;

public class CheckBox
{
    public bool IsValid {get; private set;}
    private Rectangle rect;
    private Color checkedColor = Color.DarkGray;
    private Color uncheckedColor = Color.White;
    private bool isHovered=false;
    private Color color=> IsValid ? checkedColor : uncheckedColor;
    public CheckBox(Vector2 position, int size, bool isValid)
    {
        IsValid = isValid;
        rect = new Rectangle((int)position.X, (int)position.Y, size, size);


    }
    
    public void Update()
    {
        isHovered = false;
        if (Raylib.CheckCollisionPointRec(GameState.Instance.Mouse.MousePos, rect))
        {
            isHovered = true;
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                IsValid = !IsValid;
            }
        }
    }
        
    public void Draw()
    {
        
        
        if (isHovered)
        {
            Raylib.DrawRectangleRec(rect, new Color((int)color.R, (int)color.G, (int)color.B, 120));
        }
        else
        {
            Raylib.DrawRectangleRec(rect, color);
        }
        Raylib.DrawRectangleLinesEx(rect,1, Color.Black);
    }
}