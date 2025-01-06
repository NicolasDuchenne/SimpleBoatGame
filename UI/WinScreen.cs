using Raylib_cs;
public class WinScreen
{
    public string NextLevel {get; private set;}
    private Button changeLevel;
    private ButtonsList buttonsList = new ButtonsList();
    public WinScreen(string nextLevel)
    {
        int buttonWidth = 200;
        int buttonHeight = 60;
        NextLevel = nextLevel;
        changeLevel = new Button {Rect = new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), (int)((GameState.Instance.GameScreenHeight-buttonHeight) * 0.5), buttonWidth, buttonHeight), Text = $"You Won \nClick to go to {nextLevel}", Color = Color.White};
        
        buttonsList.AddButton(changeLevel);
       
    }
    public void Update()
    {
        if ((GameState.Instance.levelFinished)&(GameState.Instance.playerDead==false))
        {
            buttonsList.Update();
            if (changeLevel.IsClicked)
            {
                GameState.Instance.playerDead=false;
                GameState.Instance.changeScene(NextLevel);
            }
        }
        
    }
    public void Draw()
    {
        if ((GameState.Instance.levelFinished)&(GameState.Instance.playerDead==false))
            buttonsList.Draw();
    }
}