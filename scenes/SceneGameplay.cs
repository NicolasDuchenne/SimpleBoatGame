using System.Numerics;
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

    private bool isPaused = false;
    private bool showRestartLevel => timer>2*maxTimer? true : false;
    private Button restartLevelButton;

    private DeathScreen deathScreen;

    private WinScreen winScreen;
    
    protected GridMap gridMap;
    private UI UI;

    public SceneGameplay(string scene_name): base(scene_name)
    {
        UI = new UI(name);
        Save.Instance.levelsScore[name] = new Score();
        restartLevelButton = new Button(
            new Rectangle(GameState.Instance.GameScreenWidth-150, 50, 100, 50),
            $"Press R to\nrestart level",
            Color.White,
            10,
            true);

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
        if (isPaused)
        {
            Raylib.DrawTextEx(Raylib.GetFontDefault(), "Paused", new Vector2(50, 50), 30, 1, Color.Black);
        }
        if (showRestartLevel)
        {
            restartLevelButton.Draw();
        }
    }

    private void UpdateMaxTurnInThePast()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.One))
            GameState.Instance.MaxTurnInPast =1;
        if (Raylib.IsKeyPressed(KeyboardKey.Two))
            GameState.Instance.MaxTurnInPast =2;
        if (Raylib.IsKeyPressed(KeyboardKey.Three))
            GameState.Instance.MaxTurnInPast =3;
        if (Raylib.IsKeyPressed(KeyboardKey.Four))
            GameState.Instance.MaxTurnInPast =4;
        if (Raylib.IsKeyPressed(KeyboardKey.Five))
            GameState.Instance.MaxTurnInPast =5;
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

    private void UpdateGame()
    {
        UpdateMaxTurnInThePast();
        timer+=Raylib.GetFrameTime();
        
#if DEBUG
        GameState.Instance.debugMagic.AddOption("timer", timer);
#endif
        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            scenesManager.changeScene("menu");
        }
        if (Raylib.IsKeyPressed(KeyboardKey.R))
        {
            scenesManager.changeScene(name);
        }
        Entity.UpdateAll();
        gridMap.Update();
        GameState.Instance.debugMagic.AddOption("Enemy Number", GameState.Instance.enemyNumber);

        deathScreen.Update();
        winScreen.Update(name);
        UI.Update();
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            isPaused = true;
        }
    }
    private void UpdatePause()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            isPaused = false;
        }
    }

    public override void Update()
    {
        base.Update();
        if(isPaused)
            UpdatePause();
        else
            UpdateGame();
        
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
        timer = 0;
        
    }
}