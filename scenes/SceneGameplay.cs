using Raylib_cs;

public class SceneGameplay : Scene
{
    protected int gridMapSize = 60;
    protected int gridMapRangeSendInPast = 1;
    private float timer;
    protected float maxTimer= 10;
    protected int maxMoves= 4;
    protected int maxSendToPast = 1;
    protected int maxElemInPast = 1;
    protected string jsonMatrix;

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

    protected void InitLevelScore()
    {
         Save.Instance.levelsScore[name].InitScore(maxTimer, maxMoves, maxSendToPast);
    }

    public override void Update()
    {
        base.Update();
        UpdateMaxTurnInThePast();
        timer+=Raylib.GetFrameTime();
#if DEBUG
        GameState.Instance.debugMagic.AddOption("timer", timer);
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
        Entity.ClearEntity();   
        deathScreen = new DeathScreen(name);
        winScreen = new WinScreen(next_scene, name);
        GameState.Instance.MaxElemInPast = 1;
        Score.Instance.ResetScore();
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create(gridMapSize, gridMapRangeSendInPast);
        Score.Instance.InitScore(maxTimer, maxMoves, maxSendToPast);
        GameState.Instance.MaxElemInPast = maxElemInPast;
        
    }
}