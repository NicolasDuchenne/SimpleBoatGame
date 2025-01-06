using System.Numerics;
using Raylib_cs;

public class Sprite
{
    public Texture2D Texture {get; private set;}

    public int Width {get; private set;}
    public int Height {get; private set;}

    public int Fps {get; private set;}
    public bool FinishedAnimation { get; private set;} = false;

    private int nFrame = 0;
    private int currentFrame = 0;
    private List<Rectangle> framePos = new List<Rectangle>();

    private float timer = 0;
    public Sprite(Texture2D texture, int nCol = 1, int nRow = 1, int fps = 1, int? width = null, int? height = null, int? startFrame = null, int? endFrame = null)
    {
        int frame = 0;
        Texture = texture;
        Fps = fps;
        if (width is null)
            Width = Texture.Width;
        else
            Width = (int)width;
        if (height is null)
            Height = Texture.Height;
        else
            Height = (int)height;
        for (int row = 0; row < nRow; row ++)
        {
            for (int col = 0; col < nCol; col ++)
            {
                if (((startFrame is not null) & (frame >= startFrame) & (frame <= endFrame)) || startFrame is null)
                {
                    framePos.Add(new Rectangle(col*Width,row*Width, Width, Height));
                    nFrame ++;
                }
                frame ++;
            }
        }
        
    }

    public static Sprite SpriteFromConfig(Dictionary<string, object> config)
    {
        Sprite sprite;
        if (config.ContainsKey("width"))
        {
            sprite = new Sprite(
                (Texture2D)config["texture"],
                (int)config["nCol"],
                (int)config["nRow"], 
                (int)config["fps"], 
                (int)config["width"], 
                (int)config["height"]
            );
        }
        else
        {
            sprite = new Sprite((Texture2D)config["texture"]);
        }
        return sprite;
    }

    public void DrawCentre(Texture2D texture, Vector2 position, float angle, Color Color, bool flip)
    {
        Rectangle img_rect_source = framePos[currentFrame];
        Rectangle img_rect_dest = new Rectangle(position.X,position.Y, Width, Height);
        if (flip)
            img_rect_source.Width*=-1;
        Raylib.DrawTexturePro(
            texture,
            img_rect_source,
            img_rect_dest,
            new Vector2((float)Width/2, (float)Height/2),
            (float)angle,
            Color);
    }

    public void Update()
    {
        if (nFrame > 1)
        {
            timer+=Raylib.GetFrameTime();
            if (timer > 1/(float)Fps)
            {
                FinishedAnimation = false;
                timer = 0;
                currentFrame ++;
                if (currentFrame >= nFrame)
                {
                    currentFrame = 0;
                    FinishedAnimation = true;
                }
                    
            }
        }
        
    }

    public void Draw(Vector2 position, float angle, Color color, bool flip)
    {
        DrawCentre(Texture, position, angle, color, flip);
    }
}