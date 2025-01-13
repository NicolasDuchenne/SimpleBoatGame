using Raylib_cs;
public class SceneMenuLevel : Scene
{


    private ButtonsList levelButtonsList;
    Button backButton ;
    int buttonWidth = 200;
    int buttonHeight = 40;
    int buttonSpace = 5;

    public SceneMenuLevel(string scene_name): base(scene_name)
    {
        backButton = new Button(new Rectangle(GameState.Instance.GameScreenWidth-110, 10, 100, 20),"Retour", Color.White);
    }

    public override void Draw()
    {
        base.Draw();
        Raylib.DrawText("Levels", 5, 5, 25, Color.Black);
        levelButtonsList.Draw();
        backButton.Draw();
    }

    public override void Update()
    {

        base.Update();
        levelButtonsList.Update();
        backButton.Update();
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
            Button tmpButton= new LevelButton(new Rectangle((int)((GameState.Instance.GameScreenWidth-buttonWidth) * 0.5), 40+i*(buttonHeight + buttonSpace), buttonWidth, buttonHeight),  $"Level {i+1}",  Color.White, Save.Instance.levelsScore[(i+1).ToString()]);  
            levelButtonsList.AddButton(tmpButton); 
        }
    }
}