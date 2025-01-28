using System.Numerics;
using Raylib_cs;
public class Power
{
    public static Sprite powerSendToAnotherDimension =  new Sprite(Raylib.LoadTexture("ressources/images/png/powerSendToAnotherDimension.png"));
    public Rectangle Rect { get; set; }
    public Sprite Sprite {get; private set;}
    public Color Color {get; private set;}
    private Vector2 position;
    private Vector2 circlePosition;
    private bool isHovered = false;
    private Button buttonHelp;
    private int powerUse=0;
    private int maxPowerUse;
    private string powerUseLeft;
    private int size;
    private int powerUseFontSize = 20;
    private Vector2 powerUseLeftLength;
    private string turnInPast;
    private Vector2 turnInPastLength;
    private int turnInPastFontSize=20;
    private string helpText ;
    private string numberOfPowerUseText;
    private float circleRadius;
    public Power(Vector2 position, int size, Sprite sprite, Vector2 helpSize, Color? color = null)
    {
        this.position = position;
        circlePosition = new Vector2(position.X, position.Y-size);
        size = Math.Max(size, Math.Max(sprite.Width, sprite.Height));
        this.size = size;
        Rect = new Rectangle(new Vector2(position.X-size*0.5f, position.Y-size*0.5f), new Vector2(size, size));
        Sprite = sprite;
        if (color is null)
            Color = Color.White;
        else
            Color = (Color)color;
        helpText = "Send Entity to another Dimension";
        numberOfPowerUseText = "You can send ";
        buttonHelp = new Button(
            new Rectangle(position + new Vector2(powerUseFontSize, -size-helpSize.Y), helpSize.X, helpSize.Y),
            helpText,
            Color,
            10,
            true
        );

        circleRadius=powerUseFontSize*0.7f;
    }

    public void Update()
    {
        
        powerUse = GameState.Instance.elemInPast;
        maxPowerUse = GameState.Instance.MaxElemInPast;
        turnInPast = GameState.Instance.MaxTurnInPast.ToString();
        turnInPastLength = Raylib.MeasureTextEx(Raylib.GetFontDefault(), turnInPast, turnInPastFontSize, 1);
        powerUseLeft = (maxPowerUse - powerUse).ToString();
        powerUseLeftLength = Raylib.MeasureTextEx(Raylib.GetFontDefault(), powerUseLeft, powerUseFontSize, 1);
        isHovered = false;
        if (Raylib.CheckCollisionPointRec(GameState.Instance.Mouse.MousePos, Rect))
        {
            isHovered = true;
            buttonHelp.UpdateText(helpText + $" for {turnInPast} turns");
        }
        if(Raylib.CheckCollisionPointCircle(GameState.Instance.Mouse.MousePos,circlePosition,circleRadius))
        {
            isHovered = true;
            buttonHelp.UpdateText(numberOfPowerUseText + $"{powerUseLeft} entity to another dimension");
        }
        
        
        
    }
    public void Draw()
    {
        Raylib.DrawRectangleRec(Rect, Color);
        Raylib.DrawRectangleLinesEx(Rect,1, Color.Black);
        Sprite.Draw(position,0, Color.White, false);
        Raylib.DrawCircle((int)circlePosition.X, (int)circlePosition.Y, circleRadius, Color);
        Raylib.DrawText($"{powerUseLeft}", (int)(position.X-powerUseLeftLength.X*0.5f), (int)(position.Y-size-powerUseLeftLength.Y*0.5f), powerUseFontSize, Color.Black);
        if (powerUse == maxPowerUse)
        {
            Raylib.DrawRectangleRec(Rect, new Color(0,0,0,200));
        }
        if (isHovered)
        {
            buttonHelp.Draw();
        }
        Raylib.DrawText($"{turnInPast}", (int)(position.X-turnInPastLength.X*0.5f), (int)(position.Y-turnInPastLength.Y*0.45f), turnInPastFontSize, Color.Black);
        
    }
}

