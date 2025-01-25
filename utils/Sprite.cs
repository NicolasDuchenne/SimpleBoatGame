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

    private bool hasShader = false;
    public bool shaderActivated = false;
    private Shader shader;
    private int timeLoc;
    private int texture0Loc;
    private int alphaLoc;

    private float time = 0.0f;

    public Sprite(Texture2D texture,Shader? shader = null, int nCol = 1, int nRow = 1, int fps = 1, int? width = null, int? height = null, int? startFrame = null, int? endFrame = null)
    {
  
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
        InitFrames(nRow, nCol, startFrame, endFrame);
        InitShader(shader);
    }

    public void InitFrames(int nRow, int nCol, int? startFrame, int? endFrame)
    {
        int frame = 0;
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

    public void InitShader(Shader? shaderConfig)
    {
        if (shaderConfig is null)
        {
            hasShader = false;
        }
        else
        {
            shader = (Shader)shaderConfig;
            hasShader = true;
            timeLoc = Raylib.GetShaderLocation(shader, "time");
            texture0Loc = Raylib.GetShaderLocation(shader, "texture0");
            alphaLoc = Raylib.GetShaderLocation(shader, "alpha");
            Raylib.SetShaderValueTexture(shader, texture0Loc, Texture);
        }
    }

    public static Sprite SpriteFromConfig(Dictionary<string, object> config)
    {
        Sprite sprite;
        if (config.ContainsKey("width"))
        {
            sprite = new Sprite(
                (Texture2D)config["texture"],
                (Shader)config["shader"],
                (int)config["nCol"],
                (int)config["nRow"], 
                (int)config["fps"], 
                (int)config["width"], 
                (int)config["height"]
            );
        }
        else
        {
            sprite = new Sprite((Texture2D)config["texture"], (Shader)config["shader"]);
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

    public virtual void Update()
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
        if (hasShader)
        {
            time+=Raylib.GetFrameTime();
            Raylib.SetShaderValue(shader, timeLoc, new float[] { time }, ShaderUniformDataType.Float);
        }
        
    }

    public virtual void Draw(Vector2 position, float angle, Color color, bool flip)
    {
        if (shaderActivated)
        {
            Raylib.SetShaderValue(shader, alphaLoc, new float[] { color.A/255.0f }, ShaderUniformDataType.Float);//I send the alpha because the sahder did not getting automatically
            Raylib.BeginShaderMode(shader);
        }
        DrawCentre(Texture, position, angle, color, flip);
        if (shaderActivated)
        {
            Raylib.EndShaderMode();
        }
            
    }

    public void ActivateShader()
    {
        if (hasShader)
        {
            if (shaderActivated==false)
            {
                time = 0;
            }
            shaderActivated = true;
        }

            
    }
    public void DeactivateShader()
    {
        shaderActivated = false;
    }

}

