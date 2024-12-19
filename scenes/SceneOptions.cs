using System.Numerics;
using Raylib_cs;
public class SceneOptions : Scene
{
    Button backButton = new Button {Rect = new Rectangle(10, 90, 200, 40), Text = "Retour", Color = Color.White};
    Button okButton = new Button {Rect = new Rectangle(215, 90, 200, 40), Text = "Save options", Color = Color.White};
    private ButtonsList buttonsList = new ButtonsList();

    private bool isFullScreen;

    public SceneOptions()
    {
        buttonsList.AddButton(backButton);
        buttonsList.AddButton(okButton);
        isFullScreen = GameState.Instance.fullScreen;
    }

    public override void Draw()
    {
        base.Draw();
        Raylib.DrawText("Options", 5, 5, 25, Color.Black);
        int screenWidth = GameState.Instance.GameScreenWidth;
        Raylib.DrawLine(0,30, screenWidth, 30, Color.Black);
        int percent = (int)(GameState.Instance.masterVolume * 100);
        Raylib.DrawText($"Volume: {percent}%", 10, 35, 20, Color.Black);
        string bFull = "Non";
        if (isFullScreen)
        {
            bFull = "Vrai";
        }
        Raylib.DrawText($"Plein ecran: {bFull}", 10, 60, 20, Color.Black);
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
            OptionsFile optionsFile = new OptionsFile();
            optionsFile.AddOption("volume", GameState.Instance.masterVolume);
            optionsFile.AddOption("fullScreen", isFullScreen);
            var testObject = new {mana = 100, arrows = 10, life = 200};
            optionsFile.AddOption("inventory", testObject);
            optionsFile.Save();
        }
        GameState.Instance.debugMagic.AddOption("full screen", isFullScreen);
        GameState.Instance.debugMagic.AddOption("game state full screen", GameState.Instance.fullScreen);
    }
}