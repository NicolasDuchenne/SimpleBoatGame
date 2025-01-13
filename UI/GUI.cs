using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

public class Button
{
    public Rectangle Rect { get; set; }
    public string Text { get; set;} = "";
    public int textSize = 10;
    public Color Color { get; set;}
    public Color OriginalColor { get; set;}
    public bool IsClicked { get; set;} = false;

    public Button(Rectangle rect, string text,Color color, int textSize= 10)
    {
        Rect = rect;
        Text = text;
        this.textSize = textSize;
        Color = color;
        OriginalColor = Color;
    }

    public void Update()
    {
            IsClicked = false;
            if (Raylib.CheckCollisionPointRec(GameState.Instance.Mouse.MousePos, Rect))
            {
                Color = Color.LightGray;
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    IsClicked = true;
                }
            }
            else
            {
                Color = OriginalColor;
            }
    }
    public virtual void Draw()
    {
        Raylib.DrawRectangleRec(Rect, Color);
        Raylib.DrawRectangleLinesEx(Rect, 2, Color.Black); 
        Raylib.DrawText(Text, (int)Rect.X +10, (int)(Rect.Y+(Rect.Size.Y - textSize)/2), textSize, Color.Black);
        
    }
}

public class LevelButton: Button
{
    Score score;
    public LevelButton(Rectangle rect, string text,Color color, Score score, int textSize= 10):base(rect, text, color,textSize)
    {
        this.score = score;
    }

    public override void Draw()
    {
        base.Draw();
        score.StarTimer.Draw(Rect.Position + new Vector2(80, Rect.Height/2), 0, Color.White, false);
        score.StarMoves.Draw(Rect.Position + new Vector2(120, Rect.Height/2), 0, Color.White, false);
        score.StarPast.Draw(Rect.Position + new Vector2(160, Rect.Height/2), 0, Color.White, false);
    
    }
}

public class ButtonsList
{
    public List<Button> buttons {get; private set;}= new List<Button>();
    public void AddButton(Button button)
    {
        buttons.Add(button);
    }
    public void Update()
    {
        foreach(Button button in buttons)
        {  
            button.Update();
        }
    }

    public void Draw()
    {
        foreach (Button button in buttons)
        {
            button.Draw();
        }
    }
}