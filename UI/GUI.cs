using Raylib_cs;
using System.Numerics;

public class Button
{
    public Rectangle Rect { get; set; }
    public string Text { get; set;} = "";
    public int textSize = 10;
    public Color Color { get; set;}
    public Color OriginalColor { get; set;}
    public bool IsClicked { get; set;} = false;
}

public class ButtonsList
{
    private List<Button> buttons = new List<Button>();
    public void AddButton(Button button)
    {
        button.OriginalColor = button.Color;
        buttons.Add(button);
    }
    public void Update()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        mousePos.X -= GameState.Instance.XOffset;
        mousePos.X /= GameState.Instance.Scale;
        mousePos.Y -= GameState.Instance.YOffset;
        mousePos.Y /= GameState.Instance.Scale;
        foreach(Button button in buttons)
        {
            button.IsClicked = false;
            if (Raylib.CheckCollisionPointRec(mousePos, button.Rect))
            {
                button.Color = Color.LightGray;
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    button.IsClicked = true;
                }
            }
            else
            {
                button.Color = button.OriginalColor;
            }
            
        }
    }

    public void Draw()
    {
        foreach (Button button in buttons)
        {
           Raylib.DrawRectangleRec(button.Rect, button.Color);
           Raylib.DrawRectangleLinesEx(button.Rect, 2, Color.Black); 
           Raylib.DrawText(button.Text, (int)button.Rect.X +10, (int)(button.Rect.Y+(button.Rect.Size.Y - button.textSize)/2), button.textSize, Color.Black);
        }
    }
}