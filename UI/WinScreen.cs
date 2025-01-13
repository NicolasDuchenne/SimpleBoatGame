using Raylib_cs;
public class WinScreen
{
    public string NextLevel {get; private set;}
    private Button changeLevel;
    private ButtonsList buttonsList = new ButtonsList();
    private float winTimer = 0;
    public WinScreen(string nextLevel)
    {
        int buttonWidth = 200;
        int buttonHeight = 60;
        NextLevel = nextLevel;
        changeLevel = new Button(new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), (int)((GameState.Instance.GameScreenHeight-buttonHeight) * 0.5), buttonWidth, buttonHeight), $"You Won \nClick to save and go to {nextLevel}", Color.White);
        
        buttonsList.AddButton(changeLevel);
       
    }
    public void Update(string level_name)
    {
        if ((GameState.Instance.levelFinished)&(GameState.Instance.playerDead==false))
        {
            winTimer +=Raylib.GetFrameTime();
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
                if ((GameState.Instance.levelFinished)&(GameState.Instance.playerDead==false))
                {
                    Save.Instance.levelsScore[level_name].updateBestScore(Score.Instance);
                }
                GameState.Instance.playerDead=false;
                GameState.Instance.changeScene(NextLevel);
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