using System.Numerics;
using Raylib_cs;
public class Water
{
    // Texture2D texture;
    private Shader shader;
    private int timeLoc;
    private int windowWidthLoc;
    private int windowHeightLoc;
    private float time;
    public Water()
    {
        time = 0;
        // Generate a radial gradient texture in code
        // Image gradientImage = Raylib.GenImageGradientRadial(256, 256, 0.5f, Raylib_cs.Color.Blue, Raylib_cs.Color.White);
        // texture = Raylib.LoadTextureFromImage(gradientImage);
        // Raylib.UnloadImage(gradientImage);
         // Load shader with default vertex shader and custom fragment shader
        // The file needs to be copied to build directory, this is done in .csproj file 
        shader = Raylib.LoadShader(null, "ressources/Shaders/water.fs");

        timeLoc = Raylib.GetShaderLocation(shader, "time");
        windowWidthLoc = Raylib.GetShaderLocation(shader, "windowWidth");
        windowHeightLoc = Raylib.GetShaderLocation(shader, "windowHeight");

        // Set the texture uniform to texture unit 0
        Raylib.SetShaderValue(shader, windowWidthLoc, new float[] { GameState.Instance.GameScreenWidth }, ShaderUniformDataType.Float);
        Raylib.SetShaderValue(shader, windowHeightLoc, new float[] { GameState.Instance.GameScreenHeight }, ShaderUniformDataType.Float);
    
    }
    public void Draw()
    {
        Raylib.BeginShaderMode(shader);
        //Raylib.DrawTexture(texture, 0, 0, Raylib_cs.Color.White);
        // Draw a full-screen rectangle with the texture

        Raylib.DrawRectangle(0, 0, GameState.Instance.GameScreenWidth,  GameState.Instance.GameScreenHeight, Color.White);

        //Raylib.DrawTexturePro(texture, sourceRect, destRect, new Vector2(0, 0), 0.0f, Raylib_cs.Color.White);
        
        Raylib.EndShaderMode();
    }

    public void Update()
    {
        // Update time
        time += Raylib.GetFrameTime();
        Raylib.SetShaderValue(shader, timeLoc, new float[] { time }, ShaderUniformDataType.Float);
    }
}