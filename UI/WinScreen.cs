using Raylib_cs;
public class WinScreen
{
    private string nextLevel;
    private string currentLevel;
    private Button changeLevel;
    private Button restartLevel;
    private bool score_updated;
    private ButtonsList buttonsList = new ButtonsList();
    private float winTimer = 0;
    public WinScreen(string nextLevel, string currentLevel)
    {
        int buttonWidth = 200;
        int buttonHeight = 60;
        score_updated = false;
        this.nextLevel = nextLevel;
        this.currentLevel = currentLevel;
        changeLevel = new Button(new Rectangle((int)((GameState.Instance.GameScreenWidth-2*buttonWidth) * 0.5 -20), (int)((GameState.Instance.GameScreenHeight+buttonHeight) * 0.5), buttonWidth, buttonHeight), $"Go to Level {nextLevel}", Color.White);
        restartLevel = new Button(new Rectangle((int)(GameState.Instance.GameScreenWidth * 0.5+20), (int)((GameState.Instance.GameScreenHeight+buttonHeight) * 0.5), buttonWidth, buttonHeight), $"Restart level", Color.White);
        buttonsList.AddButton(changeLevel);
        buttonsList.AddButton(restartLevel);
       
    }
    public void Update(string level_name)
    {
        if ((GameState.Instance.levelFinished)&(GameState.Instance.playerDead==false))
        {
            winTimer +=Raylib.GetFrameTime();
            if (score_updated == false)
            {
                score_updated = true;
                Save.Instance.levelsScore[level_name].updateBestScore(Score.Instance);
                if(int.Parse(level_name) == GameState.Instance.maxCurrentLevel)
                {
                    GameState.Instance.maxCurrentLevel ++;
                }
                Save.Instance.SaveGame();
            }
        }
        else
        {
            Score.Instance.Update();
        }
        if(winTimer >0.5)
        {
            buttonsList.Update();

            if (changeLevel.IsClicked)
            {
                GameState.Instance.changeScene(nextLevel);
            }
            else if (restartLevel.IsClicked)
            {
                GameState.Instance.changeScene(currentLevel);
            }
        }
        
    }
    public void Draw()
    {
        if(winTimer >0.5)
        {
            buttonsList.Draw();
            Score.Instance.Draw();
        }   

    }
}