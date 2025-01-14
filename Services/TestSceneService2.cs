using Raylib_cs;

public class TestSceneService2: SceneService
{
    public override void Load()
    {

    }
    public override void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            Services.Get<ISceneManagerService>().Load<TestSceneService>();
        }
        
    }
    public override void Draw()
    {
        Raylib.DrawText("test2", 0, 0, 20, Color.Black);
    }
    public override void Unload()
    {

    }
}