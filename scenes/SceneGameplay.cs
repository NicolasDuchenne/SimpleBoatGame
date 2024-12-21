using Raylib_cs;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
public class SceneGameplay : Scene
{

    private float timer;

    private Sprite spriteBoat = new Sprite(Raylib.LoadTexture("images/png/boat.png"));
    private Sprite spriteBaril = new Sprite(Raylib.LoadTexture("images/png/Baril.png"));
    private Sprite spriteObstacle = new Sprite(Raylib.LoadTexture("images/png/Obstacle.png"));
    private Sprite spriteFregate = new Sprite(Raylib.LoadTexture("images/png/Fregate.png"), 1, 6, 6, 32, 32);
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
        new GridEntity(spriteObstacle, 4, 4);
        new MovableGridEntity(spriteBaril, 5, 2);
        new MovableGridEntity(spriteBaril, 6, 4);

        new EnemyGridEntity(spriteFregate, 0, 0);
        new Player(spriteBoat, 2, 2);
        
    }
}