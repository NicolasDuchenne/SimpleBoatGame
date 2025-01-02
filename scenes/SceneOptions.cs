using System.Numerics;
using Raylib_cs;
public class SceneOptions : Scene
{
    int buttonWidth = 120;
    int buttonHeight = 20;
    int buttonSpace = 5;
    Button backButton ;
    Button okButton;
    Button deleteSaveButton;
    
    private ButtonsList buttonsList = new ButtonsList();

    private bool isFullScreen;

    public SceneOptions()
    {
        backButton = new Button {Rect = new Rectangle(10, 65, buttonWidth, buttonHeight), Text = "Retour", Color = Color.White};
        okButton = new Button {Rect = new Rectangle(10 + (buttonWidth+ buttonSpace), 65, buttonWidth, buttonHeight), Text = "Save options", Color = Color.White};
        deleteSaveButton = new Button {Rect = new Rectangle(10 + 2*(buttonWidth+ buttonSpace), 65, buttonWidth, buttonHeight), Text = "Delete Save", Color = Color.White};
            
        buttonsList.AddButton(backButton);
        buttonsList.AddButton(okButton);
        buttonsList.AddButton(deleteSaveButton);
        isFullScreen = GameState.Instance.fullScreen;
    }

    public override void Draw()
    {
        base.Draw();
        Raylib.DrawText("Options", 5, 5, 15, Color.Black);
        int screenWidth = GameState.Instance.GameScreenWidth;
        Raylib.DrawLine(0,25, screenWidth, 25, Color.Black);
        int percent = (int)(GameState.Instance.masterVolume * 100);
        Raylib.DrawText($"Volume: {percent}%", 10, 35, 10, Color.Black);
        string bFull = "Non";
        if (isFullScreen)
        {
            bFull = "Vrai";
        }
        Raylib.DrawText($"Plein ecran (press F to toggle): {bFull}", 10, 50, 10, Color.Black);
        buttonsList.Draw();

    }

    public override void Update()
    {
        base.Update();
        buttonsList.Update();

        if (Raylib.IsKeyPressed(KeyboardKey.Right))
        {
            if (GameState.Instance.masterVolume<1f)
            {
                GameState.Instance.SetVolume(GameState.Instance.masterVolume + 0.01f);
            }
        }
        if (Raylib.IsKeyPressed(KeyboardKey.Left))
        {
            if (GameState.Instance.masterVolume>0f)
            {
                GameState.Instance.SetVolume(GameState.Instance.masterVolume - 0.01f);
            }
        }
        if (Raylib.IsKeyPressed(KeyboardKey.F))
        {
            isFullScreen = !isFullScreen;
            
        }

        if  (backButton.IsClicked)
        {
            GameState.Instance.changeScene("menu");
        }
        else if (okButton.IsClicked)
        {
            
            if (isFullScreen != GameState.Instance.fullScreen)
            {
                GameState.Instance.fullScreen = isFullScreen;
                GameState.Instance.ToggleFullScreen();         
            }
            OptionsFile optionsFile = new OptionsFile(OptionsFile.OPTIONSFILENAME);
            optionsFile.AddOption("volume", GameState.Instance.masterVolume);
            optionsFile.AddOption("fullScreen", isFullScreen);
            var testObject = new {mana = 100, arrows = 10, life = 200};
            optionsFile.AddOption("inventory", testObject);
            optionsFile.Save();
        }
        else if (deleteSaveButton.IsClicked)
        {
            GameState.Instance.maxCurrentLevel = 1;
            GameState.Instance.currentLevel = "1";
            OptionsFile saveFile = new OptionsFile(OptionsFile.SAVEFILLNAME);
            saveFile.Save();
        }
        GameState.Instance.debugMagic.AddOption("full screen", isFullScreen);
        GameState.Instance.debugMagic.AddOption("game state full screen", GameState.Instance.fullScreen);
    }
}