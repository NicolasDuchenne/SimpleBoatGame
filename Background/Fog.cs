
using System.Numerics;
using Raylib_cs;
public class Fog
{
    private Texture2D texture;
    private Shader shader;
    private int timeLoc;
    private int texture0Loc;
    private int alphaLoc;
    private int scaleLoc;
    private float time;
    private int size;
    private float lerpSpeed = 15;

    public Vector2 Position {get; private set;}
    public Fog(int size)
    {
        this.size = size;
        time = 0;
        // Generate a radial gradient texture in code
        Color imageColor = new Color(255, 255, 255, 80);
        Image Image = Raylib.GenImageColor(this.size, this.size, imageColor);  // Gris uniforme
        texture = Raylib.LoadTextureFromImage(Image);
        Raylib.UnloadImage(Image);
         // Load shader with default vertex shader and custom fragment shader
        // The file needs to be copied to build directory, this is done in .csproj file 
        shader = Raylib.LoadShader(null, "ressources/Shaders/fog.fs");

        timeLoc = Raylib.GetShaderLocation(shader, "time");
        texture0Loc = Raylib.GetShaderLocation(shader, "texture0");
        alphaLoc = Raylib.GetShaderLocation(shader, "alpha");
        scaleLoc = Raylib.GetShaderLocation(shader, "scale");
        Raylib.SetShaderValueTexture(shader, texture0Loc, texture);
        Raylib.SetShaderValue(shader, alphaLoc, new float[] { imageColor.A/255f }, ShaderUniformDataType.Float);
        float scale = this.size/2;
        Raylib.SetShaderValue(shader, scaleLoc, new float[] { scale }, ShaderUniformDataType.Float);
            
        Position = new Vector2();
    }

    public void DrawCentre(Texture2D texture, Vector2 position, float angle, Color Color)
    {
        Rectangle img_rect_source = new Rectangle(0,0,texture.Width, texture.Height);
        Rectangle img_rect_dest = new Rectangle(position.X,position.Y, texture.Width, texture.Height);
        Raylib.DrawTexturePro(
            texture,
            img_rect_source,
            img_rect_dest,
            new Vector2((float)texture.Width/2, (float)texture.Height/2),
            (float)angle,
            Color);
    }
    public void Draw()
    {
        Raylib.BeginShaderMode(shader);
        DrawCentre(texture, Position, 0, Color.White);
        Raylib.EndShaderMode();
    }

    public void Update()
    {
        // Update time
        time += Raylib.GetFrameTime();
        Raylib.SetShaderValue(shader, timeLoc, new float[] { time }, ShaderUniformDataType.Float);
    }

    public void SetPositionWithLerp(Vector2 position)
    {
        Position = Utils.Lerp(Position, position, lerpSpeed*Raylib.GetFrameTime());
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }
}