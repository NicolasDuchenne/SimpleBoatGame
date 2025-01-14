using Raylib_cs;

public class SceneGameplay : Scene
{

    private float timer;

    private DeathScreen deathScreen;

    private WinScreen winScreen;
    
    protected GridMap gridMap;
    private UI UI;

    public SceneGameplay(string scene_name): base(scene_name)
    {
        UI = new UI(name);
        Save.Instance.levelsScore[name] = new Score();
    }

    public override void Draw()
    {
        base.Draw();
        //Raylib.DrawRectangleRec(new Rectangle(0, 0, GameState.Instance.GameScreenWidth, GameState.Instance.GameScreenHeight), Color.DarkBlue);
        Entity.DrawAll();
        gridMap.Draw();
        deathScreen.Draw();
        winScreen.Draw();
        UI.Draw();
    }

    private void UpdateMaxTurnInThePast()
    {
        if ((Raylib.IsKeyPressed(KeyboardKey.RightAlt)) || (Raylib.IsKeyPressed(KeyboardKey.LeftControl)))
        {
            GameState.Instance.MaxTurnInPast --;
        }
        else if ((Raylib.IsKeyPressed(KeyboardKey.RightShift)) || (Raylib.IsKeyPressed(KeyboardKey.LeftShift)))
        {
            GameState.Instance.MaxTurnInPast ++;
        }
        //float zoomChange = Raylib.GetMouseWheelMove();
        //GameState.Instance.MaxTurnInPast +=(int)zoomChange;
    }

    public override void Update()
    {
        base.Update();
        UpdateMaxTurnInThePast();
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
        Entity.UpdateAll();
        gridMap.Update();
        GameState.Instance.debugMagic.AddOption("Enemy Number", GameState.Instance.enemyNumber);

        deathScreen.Update();
        winScreen.Update(name);
        UI.Update();
        
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
        GameState.Instance.enemyNumber = 0;
        base.Show();
        Entity.ALL.Clear();   
        deathScreen = new DeathScreen(name);
        winScreen = new WinScreen(next_scene);
        GameState.Instance.MaxElemInPast = 1;
        Score.Instance.ResetScore();
    }
}