using Raylib_cs;
public class SceneMenuLevel : Scene
{


    private ButtonsList levelButtonsList;
    Button backButton ;
    int buttonWidth = 200;
    int buttonHeight = 40;
    int buttonSpace = 10;

    public SceneMenuLevel(string scene_name): base(scene_name)
    {
        backButton = new Button(new Rectangle(GameState.Instance.GameScreenWidth-110, 10, 100, 20),"Retour", Color.White, 10, true);
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
            scenesManager.changeScene("menu");
        }
        for (int i=0; i<levelButtonsList.buttons.Count(); i++)
        {
            if (levelButtonsList.buttons[i].IsClicked)
            {
                scenesManager.changeScene((i+1).ToString());
            }
        } 
        if  (backButton.IsClicked)
        {
            scenesManager.changeScene("menu");
        }
        
    }
    public override void Show()
    {
        levelButtonsList = new ButtonsList();
        int col = 0;
        int pos = 0;
        int row = 0;
        for (int i=0; i<GameState.Instance.maxCurrentLevel; i++)
        {
            pos = 40+row*(buttonHeight + buttonSpace);
            if (pos+buttonHeight > GameState.Instance.GameScreenHeight)
            {
                row = 0;
                col++;
                pos = 40+row*(buttonHeight + buttonSpace);
            }
            Button tmpButton= new LevelButton(new Rectangle(col*(buttonWidth+20)+20, pos, buttonWidth, buttonHeight),  $"Level {i+1}",  Color.White, Save.Instance.levelsScore[(i+1).ToString()]);  
            levelButtonsList.AddButton(tmpButton); 
            row ++;
        }
    }
}