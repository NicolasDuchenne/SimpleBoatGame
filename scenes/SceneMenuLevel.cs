using Raylib_cs;
public class SceneMenuLevel : Scene
{


    private ButtonsList levelButtonsList;
    private ButtonsList buttonList= new ButtonsList();
    Button backButton ;


    int buttonWidth = 100;
    int buttonHeight = 20;
    int buttonSpace = 5;

    public SceneMenuLevel(string scene_name): base(scene_name)
    {
        backButton = new Button {Rect = new Rectangle(GameState.Instance.GameScreenWidth-buttonWidth-10, 10, buttonWidth, buttonHeight), Text = "Retour", Color = Color.White};
        buttonList.AddButton(backButton);

    }

    public override void Draw()
    {
        base.Draw();
        Raylib.DrawText("Levels", 5, 5, 25, Color.Black);
        levelButtonsList.Draw();
        buttonList.Draw();
    }

    public override void Update()
    {

        base.Update();
        levelButtonsList.Update();
        buttonList.Update();
        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            GameState.Instance.changeScene("menu");
        }
        for (int i=0; i<levelButtonsList.buttons.Count(); i++)
        {
            if (levelButtonsList.buttons[i].IsClicked)
            {
                GameState.Instance.changeScene((i+1).ToString());
            }
        } 
        if  (backButton.IsClicked)
        {
            GameState.Instance.changeScene("menu");
        }
        
    }
    public override void Show()
    {
        levelButtonsList = new ButtonsList();
        for (int i =0; i<GameState.Instance.maxCurrentLevel; i++)
        {
            Button tmpButton= new Button {Rect = new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), 40+i*(buttonHeight + buttonSpace), buttonWidth, buttonHeight), Text = $"Level {i+1}", Color = Color.White};  
            levelButtonsList.AddButton(tmpButton); 
        }
    }
}