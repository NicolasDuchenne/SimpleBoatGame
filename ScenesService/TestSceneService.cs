using Raylib_cs;

public class TestSceneService: SceneService
{
    public override void Load()
    {

    }
    public override void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            Services.Get<ISceneManagerService>().Load<TestSceneService2>();
        }
    }
    public override void Draw()
    {
        Raylib.DrawText("test", 0, 0, 20, Color.Black);
    }
    public override void Unload()
    {

    }
}