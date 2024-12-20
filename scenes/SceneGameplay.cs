using Raylib_cs;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
public class SceneGameplay : Scene
{

    private float timer;

    private Texture2D texBoat = Raylib.LoadTexture("images/png/boat.png");
    private Texture2D texBaril = Raylib.LoadTexture("images/png/Baril.png");
    private GridMap gridMap;
    int columnNumber = 10;
    int rowNumber = 10;
    int size = 40;

    public override void Draw()
    {
        base.Draw();
        Raylib.DrawRectangleRec(new Rectangle(0, 0, GameState.Instance.GameScreenWidth, GameState.Instance.GameScreenHeight), Color.DarkBlue);
        gridMap.Draw();
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
        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            GameState.Instance.changeScene("menu");
        }
        Entity.UpdateAll();
        GameState.Instance.debugMagic.AddOption("size", size);
    }

    public override void Show()
    {
        Random rnd = new Random();
        base.Show();
        Entity.ALL.Clear();   
        gridMap = new GridMap(columnNumber, rowNumber, size);
        GameState.Instance.SetGridMap(gridMap);
        new GridEntity(texBaril, 4, 4);
        new MovableGridEntity(texBaril, 5, 2);
        new MovableGridEntity(texBaril, 6, 2);
        new Player(texBoat, 2, 2);
        
    }
}