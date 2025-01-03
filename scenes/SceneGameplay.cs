using Raylib_cs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;
public class SceneGameplay : Scene
{

    private float timer;

    private DeathScreen deathScreen;

    private WinScreen winScreen;
    
    protected GridMap gridMap;

    public SceneGameplay():base()
    {
        
    }

    public override void Draw()
    {
        base.Draw();
        //Raylib.DrawRectangleRec(new Rectangle(0, 0, GameState.Instance.GameScreenWidth, GameState.Instance.GameScreenHeight), Color.DarkBlue);
        Entity.DrawAll();
        gridMap.Draw();
        deathScreen.Draw();
        winScreen.Draw();
        Timers.Instance.Draw();
        Raylib.DrawText($"Level {name}", (int)(GameState.Instance.GameScreenWidth*0.5), 5, 25, Color.Black);
    }

    public override void Update()
    {
        base.Update();
        Timers.Instance.Update();
        timer+=Raylib.GetFrameTime();
#if DEBUG
        GameState.Instance.debugMagic.AddOption("timer", timer);
        GameState.Instance.debugMagic.AddOption("Entities", Entity.ALL.Count);
#endif
        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            GameState.Instance.changeScene("menu");
        }
        if (Raylib.IsKeyPressed(KeyboardKey.R))
        {
            GameState.Instance.changeScene(name);
        }
        gridMap.Update();
        Entity.UpdateAll();
        GameState.Instance.debugMagic.AddOption("Enemy Number", GameState.Instance.enemyNumber);
        if (GameState.Instance.enemyNumber == 0)
        {
            GameState.Instance.levelFinished = true;
        }

        deathScreen.Update();
        winScreen.Update();
    }

    public override void Show()
    {
        GameState.Instance.currentLevel = name;
        int currentLevelInt = int.Parse(name);
        if (GameState.Instance.maxCurrentLevel < currentLevelInt)
        {
            GameState.Instance.maxCurrentLevel = currentLevelInt;
        }
        
        GameState.Instance.elemInPast = 0;
        GameState.Instance.levelFinished = false;
        GameState.Instance.enemyNumber = 0;
        base.Show();
        Entity.ALL.Clear();   
        deathScreen = new DeathScreen(name);
        winScreen = new WinScreen(next_scene);
    }
}