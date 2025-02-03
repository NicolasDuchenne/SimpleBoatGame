using Raylib_cs;

public class GameState
{
    private static GameState? instance;
    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameState();
            }
            return instance;
        }
    }
    public int GameScreenWidth {get; private set;}
    public int GameScreenHeight {get; private set;}
    public float Scale {get; private set;} = 1;
    public float ResizedGameWidth {get; private set;} = 0;
    public float ResizedGameHeight {get; private set;} = 0;
    public float XOffset {get; private set;} = 0;
    public float YOffset {get; private set;} = 0;

    public Mouse Mouse = new Mouse();


    public DebugMagic debugMagic = new DebugMagic();

    public float masterVolume {get; private set;}
    public bool fullScreen {get; set;}

    public bool finishGame = false;

    public string currentLevel;
    public int maxCurrentLevel=1;

    public GridMap GridMap {get; private set;}
    public int gridMapDefaultSize = 60;

    public int elemInPast = 0;

    private int maxElemInPast=3;

    public int MaxElemInPast
    {
        get => maxElemInPast;
        set => maxElemInPast = Math.Clamp(value, 1, 3); 
    }

    private int maxTurnInPast=3;

    public int MaxTurnInPast
    {
        get => maxTurnInPast;
        set => maxTurnInPast = Math.Clamp(value, 1, 5);
    }

    int ecran = 0;
    int monitorWidth = 0;
    int monitorHeight = 0;

    public bool playerDead = false;
    public bool levelFinished => enemyNumber == 0;

    public int enemyNumber = 0;

    private string fontPath = "ressources/Font/JungleAdventurer.ttf";
    public Font customFont;
    public Font customFontBig;


    public void SetVirtualGameResolution(int width, int height)
    {
        GameScreenWidth = width;
        GameScreenHeight = height;
    }

    public void ChangeAspectRatio()
    {
        Scale = Math.Min((float)Raylib.GetScreenWidth()/GameScreenWidth,(float)Raylib.GetScreenHeight()/GameScreenHeight);   
        ResizedGameWidth = GameScreenWidth * Scale;
        ResizedGameHeight = GameScreenHeight * Scale;
        XOffset = (Raylib.GetScreenWidth() - ResizedGameWidth)/2;
        YOffset = (Raylib.GetScreenHeight() - ResizedGameHeight)/2;
        

        debugMagic.AddOption("scale", Scale);
        debugMagic.AddOption("xOffset", XOffset);
        debugMagic.AddOption("yOffset", YOffset);
        debugMagic.AddOption("Taille ecran", Raylib.GetScreenWidth().ToString() + "x" + Raylib.GetScreenHeight().ToString());
    }

    public void SetGridMap(GridMap gridMap)
    {
        GridMap = gridMap;
    }

    public void SetVolume(float volume)
    {
        volume = Math.Clamp(volume, 0, 1);
        masterVolume = volume;
        Raylib.SetMasterVolume(masterVolume);
    }
    public GameState()
    {
        customFont = Raylib.LoadFontEx(fontPath, 20,null,0); 
        customFontBig = Raylib.LoadFontEx(fontPath, 40,null,0);
        ecran = Raylib.GetCurrentMonitor();
        monitorWidth = Raylib.GetMonitorWidth(ecran);
        monitorHeight = Raylib.GetMonitorHeight(ecran);
        LoadOptions();
    }
    private void LoadOptions()
    {
        OptionsFile optionsFile = new OptionsFile(OptionsFile.OPTIONSFILENAME);
        optionsFile.Load();
        if (optionsFile.IsOptionExists("volume"))
        {
            float volume = optionsFile.GetOptionFloat("volume");
            masterVolume = volume;
        }
        else
        {
            masterVolume = 0.8f;
        }
        SetVolume(masterVolume);
        Console.WriteLine($"master volume {masterVolume}");
        fullScreen = optionsFile.GetOptionBool("fullScreen");
        if (fullScreen)
        {
            ToggleFullScreen();
        }
    }

    public void ToggleFullScreen()
    {
        
        
        if ((Raylib.GetScreenHeight() != monitorHeight) &  (Raylib.GetScreenWidth() != monitorWidth) | (GameScreenWidth==0))
        {
            Raylib.SetWindowPosition(0, 0);
            Raylib.SetWindowState(ConfigFlags.UndecoratedWindow);
            Raylib.SetWindowSize(monitorWidth, monitorHeight);

        }
        else
        {
            Raylib.SetWindowSize(GameScreenWidth, GameScreenHeight);
            Raylib.SetWindowPosition((monitorWidth - GameScreenWidth)/2, (monitorHeight-GameScreenHeight)/2);
            Raylib.ClearWindowState(ConfigFlags.UndecoratedWindow);
            Raylib.SetWindowState(ConfigFlags.ResizableWindow);
        } 
    }
}