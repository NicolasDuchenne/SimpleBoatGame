using System.Numerics;
using System.Security.AccessControl;
using Raylib_cs;
public class SceneOptions : Scene
{
    int buttonWidth = 140;
    int buttonHeight = 20;
    int buttonSpace = 10;
    Button backButton ;
    Button okButton;
    Button deleteSaveButton;
    CheckBox fullScreenCheckBox;
    SlidingBar volumeBar;
    
    private ButtonsList buttonsList = new ButtonsList();

    private bool isFullScreen;

    private List<string> controles =  ["CONTROLES:",
"* Up Arrow or Z: up",
"* Down Arrow or S: down",
"* Left Arrow or Q: left",
"* Right Arrow or D: right",
"* 1-5: Set number of turn for send in another dimension",
"* Alt Right or Control Left: decrease number of turn for send in another dimension",
"* Shift Right or Shift Left: increase number of turn for send in another dimension",
"* Left Click: send in past",
"* R: restart level",
"* Space: pause level",
"* Escape: go to main menu"];

    


    public SceneOptions(string scene_name): base(scene_name)
    {
        backButton = new Button(new Rectangle(10, 80, buttonWidth, buttonHeight), "Retour", Color.White, 10, true);
        okButton = new Button(new Rectangle(10 + (buttonWidth+ buttonSpace), 80, buttonWidth, buttonHeight), "Apply and Save Options", Color.White, 10, true);
        deleteSaveButton = new Button(new Rectangle(10, GameState.Instance.GameScreenHeight-50, buttonWidth, buttonHeight), "Delete Save", Color.White, 10, true);
        fullScreenCheckBox = new CheckBox(
            new Vector2(80, 57),
            15,
            GameState.Instance.fullScreen
        );
        volumeBar = new SlidingBar(
            new Vector2(80, 35),
            100,
            10,
            0f,
            1f,
            GameState.Instance.masterVolume
        );
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
        Raylib.DrawText($"Plein ecran: ", 10, 60, 10, Color.Black);
        fullScreenCheckBox.Draw();
        buttonsList.Draw();
        volumeBar.Draw();
        int i = 150;
        foreach (string line in controles)
        {
            Raylib.DrawTextEx(GameState.Instance.customFont, line, new Vector2(10, i), GameState.Instance.customFont.BaseSize, 1, Color.Black);
            i+= GameState.Instance.customFont.BaseSize+2;
        }

        

    }

    public override void Update()
    {
        base.Update();
        buttonsList.Update();
        volumeBar.Update();
        GameState.Instance.SetVolume(volumeBar.SliderValue);

        fullScreenCheckBox.Update();
        isFullScreen = fullScreenCheckBox.IsValid;

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