using Raylib_cs;
public class DeathScreen
{
    public string CurrentLevel {get; private set;}
    private Button restartButton;
    private ButtonsList buttonsList = new ButtonsList();
    private float playerDeadTimer = 0;
    public DeathScreen(string currentLevel)
    {
        int buttonWidth = 200;
        int buttonHeight = 60;
        CurrentLevel = currentLevel;
        restartButton = new Button(new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), (int)((GameState.Instance.GameScreenHeight-buttonHeight) * 0.5), buttonWidth, buttonHeight), "You Died \n Click to Restart", Color.White);
        GameState.Instance.playerDead = false;
        buttonsList.AddButton(restartButton);
       
    }
    public void Update()
    {
        if (GameState.Instance.playerDead)
        {
            playerDeadTimer +=Raylib.GetFrameTime();
        }
           
        if (playerDeadTimer>0.5)
        {
            buttonsList.Update();
            if (restartButton.IsClicked)
            {
                GameState.Instance.playerDead=false;
                GameState.Instance.changeScene(CurrentLevel);
            }
        }
        
    }
    public void Draw()
    {
        if (playerDeadTimer>0.5)
            buttonsList.Draw();
    }
}