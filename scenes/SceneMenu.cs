using Raylib_cs;
public class SceneMenu : Scene
{
    private Button playButton;
    private Button optionsButton;
    private Button quitButton;
    private Button loadButton;

    private ButtonsList buttonsList = new ButtonsList();

    public SceneMenu()
    {
        int buttonWidth = 100;
        int buttonHeight = 20;
        int buttonSpace = 5;
        playButton = new Button {Rect = new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), 40, buttonWidth, buttonHeight), Text = "Start new Game", Color = Color.White};
        loadButton = new Button {Rect = new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), 40+1*(buttonHeight + buttonSpace), buttonWidth, buttonHeight), Text = "Load", Color = Color.White};
        optionsButton = new Button {Rect = new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), 40+2*(buttonHeight + buttonSpace), buttonWidth, buttonHeight), Text = "Options", Color = Color.White};
        quitButton = new Button {Rect = new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), 40+3*(buttonHeight + buttonSpace), buttonWidth, buttonHeight), Text = "Quit", Color = Color.White};
        
        buttonsList.AddButton(playButton);
        buttonsList.AddButton(loadButton);
        buttonsList.AddButton(optionsButton);
        buttonsList.AddButton(quitButton);
    }

    public override void Draw()
    {
        base.Draw();
        Raylib.DrawText("MENU", 5, 5, 25, Color.Black);
        buttonsList.Draw();
    }

    public override void Update()
    {
        base.Update();
        buttonsList.Update();
        GameState.Instance.debugMagic.AddOption("current level", GameState.Instance.currentLevel);
        if (playButton.IsClicked)
        {
            GameState.Instance.changeScene("level 1");
        }
        if ((loadButton.IsClicked) & (GameState.Instance.currentLevel is not null))
        {
            GameState.Instance.changeScene(GameState.Instance.currentLevel);
        }
        else if  (optionsButton.IsClicked)
        {
            GameState.Instance.changeScene("options");
        }
        else if  (quitButton.IsClicked)
        {
            GameState.Instance.finishGame = true;
        }
        
    }
}