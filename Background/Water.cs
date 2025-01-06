using System.Numerics;
using Raylib_cs;
public class Water
{
    Texture2D waterTexture;
    Shader waterShader;
    int timeLoc;
    int windowWidthLoc;
    int windowHeightLoc;
    float time;
    public Water()
    {
        time = 0;
        // Generate a radial gradient texture in code
        Image gradientImage = Raylib.GenImageGradientRadial(256, 256, 0.5f, Raylib_cs.Color.Blue, Raylib_cs.Color.White);
        waterTexture = Raylib.LoadTextureFromImage(gradientImage);
        Raylib.UnloadImage(gradientImage);
         // Load shader with default vertex shader and custom fragment shader
        // The file needs to be copied to build directory, this is done in .csproj file 
        waterShader = Raylib.LoadShader(null, "ressources/Shaders/water.fs");

        timeLoc = Raylib.GetShaderLocation(waterShader, "time");
        windowWidthLoc = Raylib.GetShaderLocation(waterShader, "windowWidth");
        windowHeightLoc = Raylib.GetShaderLocation(waterShader, "windowHeight");

        // Set the texture uniform to texture unit 0
        Raylib.SetShaderValue(waterShader, windowWidthLoc, new float[] { GameState.Instance.GameScreenWidth }, ShaderUniformDataType.Float);
        Raylib.SetShaderValue(waterShader, windowHeightLoc, new float[] { GameState.Instance.GameScreenHeight }, ShaderUniformDataType.Float);
    
    }
    public void Draw()
    {
        Raylib.BeginShaderMode(waterShader);
        //Raylib.DrawTexture(waterTexture, 0, 0, Raylib_cs.Color.White);
        // Draw a full-screen rectangle with the texture

        Raylib.DrawRectangle(0, 0, GameState.Instance.GameScreenWidth,  GameState.Instance.GameScreenHeight, Color.White);

        //Raylib.DrawTexturePro(waterTexture, sourceRect, destRect, new Vector2(0, 0), 0.0f, Raylib_cs.Color.White);
        
        Raylib.EndShaderMode();
    }

    public void Update()
    {
        // Update time
        time += Raylib.GetFrameTime();
        //Console.WriteLine(Environment.CurrentDirectory);
        Raylib.SetShaderValue(waterShader, timeLoc, new float[] { time }, ShaderUniformDataType.Float);
    }
}