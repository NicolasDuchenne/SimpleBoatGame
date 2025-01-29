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
    private Vector2 textPosition;
    private Rectangle shadowBounds;
    private int shadowOffset = 4;
    private Color shadowColor = Color.DarkBlue;
    private Color hoveredColor = Color.LightGray;
    private Color clickedColor = Color.DarkGray;
    private bool textCentered;

    public Button(Rectangle rect, string text,Color color, int textSize= 10, bool textCentered = false)
    {
        Rect = rect;
        this.textCentered = textCentered;
        this.textSize = textSize;
        Color = color;
        OriginalColor = Color;
        shadowBounds = new Rectangle(Rect.X + shadowOffset, Rect.Y + shadowOffset, Rect.Width, Rect.Height);
        UpdateText(text);
        
        
    }
    public void UpdateText(string text)
    {
        Text = text;
        if(textCentered)
        {
            CenterText();
        }
        else
        {
            textPosition = new Vector2((int)Rect.X +10, (int)(Rect.Y+(Rect.Size.Y - textSize)/2));
        }
    }

    public void CenterText()
    {
        Vector2 textLength = Raylib.MeasureTextEx(Raylib.GetFontDefault(), Text, textSize, 1);
        textPosition = new Vector2(
            Rect.X + (Rect.Width - textLength.X) / 2,
            Rect.Y + (Rect.Height - textLength.Y) / 2
        );
    }

    public void Update()
    {
        IsClicked = false;
        if (Raylib.CheckCollisionPointRec(GameState.Instance.Mouse.MousePos, Rect))
        {
            Color = hoveredColor;
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                IsClicked = true;
                Color = clickedColor;
            }
        }
        else
        {
            Color = OriginalColor;
        }
    }
    public virtual void Draw()
    {
        Raylib.DrawRectangleRounded(shadowBounds, 0.5f, 10, shadowColor);
        Raylib.DrawRectangleRounded(Rect, 0.8f, 30, Color);
        Raylib.DrawRectangleRoundedLines(Rect, 0.8f, 30, 2, Color.Black); 
        
        Raylib.DrawTextEx(Raylib.GetFontDefault(), Text, textPosition, textSize, 1, Color.Black);
        //Raylib.DrawText(Text, (int)Rect.X +10, (int)(Rect.Y+(Rect.Size.Y - textSize)/2), textSize, Color.Black);
        
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