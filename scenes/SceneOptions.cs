using System.Numerics;
using System.Security.AccessControl;
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

    private List<string> controles =  ["CONTROLES:",
"* Up Arrow or Z: up",
"* Down Arrow or S: down",
"* Left Arrow or Q: left",
"* Right Arrow or D: right",
"* Alt Right or Control Left: decrease number of turn for send in past",
"* Shift Right or Shift Left: increase number of turn for send in past",
"* Left Click: send in past",
"* R: restart level",
"* Escape: go to main menu"];

    


    public SceneOptions(string scene_name): base(scene_name)
    {
        backButton = new Button(new Rectangle(10, 65, buttonWidth, buttonHeight), "Retour", Color.White);
        okButton = new Button(new Rectangle(10 + (buttonWidth+ buttonSpace), 65, buttonWidth, buttonHeight), "Save options", Color.White);
        deleteSaveButton = new Button(new Rectangle(10, GameState.Instance.GameScreenHeight-50, buttonWidth, buttonHeight), "Delete Save", Color.White);
            
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
        //Raylib.DrawText(controles, 10, 310, 10, Color.Black);
        int i = 150;
        foreach (string line in controles)
        {
            Raylib.DrawTextEx(GameState.Instance.customFont, line, new Vector2(10, i), GameState.Instance.customFont.BaseSize, 1, Color.Black);
            i+= GameState.Instance.customFont.BaseSize+2;
            // Raylib.DrawText(line, 10, i, 15, Color.Black);
            // i+= 17;
        }

        

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

        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            GameState.Instance.changeScene("menu");
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
            Save.Instance.ResetSave();
            Save.Instance.SaveGame();
        }
        GameState.Instance.debugMagic.AddOption("full screen", isFullScreen);
        GameState.Instance.debugMagic.AddOption("game state full screen", GameState.Instance.fullScreen);
    }
}