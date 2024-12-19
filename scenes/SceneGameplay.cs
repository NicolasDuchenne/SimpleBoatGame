using Raylib_cs;
using System.Numerics;
public class SceneGameplay : Scene
{

    private float timer;

    private Texture2D texBall = Raylib.LoadTexture("images/shipBlue_manned.png");
    public override void Draw()
    {
        base.Draw();
        Raylib.DrawText("Gameplay", 5, 5, 25, Color.Black);
        Entity.DrawAll();
    }
    public override void Update()
    {
        base.Update();
        timer+=Raylib.GetFrameTime();
#if DEBUG
        GameState.Instance.debugMagic.AddOption("timer", timer);
        GameState.Instance.debugMagic.AddOption("Entities", Entity.ALL.Count);
#endif
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            GameState.Instance.changeScene("menu");
        }
        Entity.UpdateAll();
    }

    public override void Show()
    {
        Random rnd = new Random();
        base.Show();
        Entity.ALL.Clear();

    }
}