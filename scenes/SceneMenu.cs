using Raylib_cs;
public class SceneMenu : Scene
{
    private Button playButton;
    private Button optionsButton;
    private Button quitButton;

    private ButtonsList buttonsList = new ButtonsList();

    public SceneMenu()
    {
        playButton = new Button {Rect = new Rectangle(10, 40, 200, 40), Text = "Play", Color = Color.White};
        optionsButton = new Button {Rect = new Rectangle(10, 85, 200, 40), Text = "Options", Color = Color.White};
        quitButton = new Button {Rect = new Rectangle(10, 130, 200, 40), Text = "Quit", Color = Color.White};

        buttonsList.AddButton(playButton);
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
        if (playButton.IsClicked)
        {
            GameState.Instance.changeScene("gameplay");
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