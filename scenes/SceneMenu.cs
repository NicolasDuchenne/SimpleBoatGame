using Raylib_cs;
public class SceneMenu : Scene
{
    private Button optionsButton;
    private Button quitButton;
    private Button resumeButton;
    private Button choseButton;

    private ButtonsList buttonsList = new ButtonsList();

    public SceneMenu(string scene_name): base(scene_name)
    {
        int buttonWidth = 120;
        int buttonHeight = 20;
        int buttonSpace = 10;
        resumeButton = new Button(new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), 40+1*(buttonHeight + buttonSpace), buttonWidth, buttonHeight), "Start",Color.White, 10, true);
        choseButton = new Button(new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), 40+2*(buttonHeight + buttonSpace), buttonWidth, buttonHeight),  "Chose Level",Color.White, 10, true);
        optionsButton = new Button(new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), 40+3*(buttonHeight + buttonSpace), buttonWidth, buttonHeight),  "Options", Color.White, 10, true);
        quitButton = new Button(new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), 40+4*(buttonHeight + buttonSpace), buttonWidth, buttonHeight), "Quit", Color.White, 10, true);
        
        buttonsList.AddButton(resumeButton);
        buttonsList.AddButton(choseButton);
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
        if (resumeButton.IsClicked)
        {
            scenesManager.changeScene(GameState.Instance.maxCurrentLevel.ToString());
        }
        else if (choseButton.IsClicked)
        {
            scenesManager.changeScene("menuLevel");
        }
        else if  (optionsButton.IsClicked)
        {
            scenesManager.changeScene("options");
        }

        else if  (quitButton.IsClicked)
        {
            GameState.Instance.finishGame = true;
        }
        
    }

    public override void Show()
    { 
        resumeButton.Text = $"Start Level {GameState.Instance.maxCurrentLevel}";
        resumeButton.CenterText();
    }
}