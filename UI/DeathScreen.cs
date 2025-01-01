using Raylib_cs;
public class DeathScreen
{
    public string CurrentLevel {get; private set;}
    private Button restartButton;
    private ButtonsList buttonsList = new ButtonsList();
    public DeathScreen(string currentLevel)
    {
        int buttonWidth = 200;
        int buttonHeight = 60;
        CurrentLevel = currentLevel;
        restartButton = new Button {Rect = new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), (int)((GameState.Instance.GameScreenHeight-buttonHeight) * 0.5), buttonWidth, buttonHeight), Text = "You Died \n Click to Restart", Color = Color.White};
        
        buttonsList.AddButton(restartButton);
       
    }
    public void Update()
    {
        if (GameState.Instance.playerDead)
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
        if (GameState.Instance.playerDead)
            buttonsList.Draw();
    }
}