using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

public class GameState
{
    private Scene? currentScene;

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

    private Dictionary<string, Scene> scenes;

    public DebugMagic debugMagic = new DebugMagic();

    public float masterVolume {get; private set;}
    public bool fullScreen {get; set;}

    public bool finishGame = false;

    public GridMap GridMap {get; private set;}

    int ecran = 0;
    int monitorWidth = 0;
    int monitorHeight = 0;
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
        masterVolume = volume;
        Raylib.SetMasterVolume(masterVolume);
    }
    public GameState()
    {
        ecran = Raylib.GetCurrentMonitor();
        monitorWidth = Raylib.GetMonitorWidth(ecran);
        monitorHeight = Raylib.GetMonitorHeight(ecran);
        scenes = new Dictionary<string, Scene>();
        OptionsFile optionsFile = new OptionsFile();
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
    public void RegisterScene(string name, Scene scene)
    {
        scene.name = name;
        scenes[name] = scene;
    }

    public void RemoveScene(string name)
    {
        if (scenes.ContainsKey(name))
        {
            scenes[name].Close();
            scenes.Remove(name);
        }
    }

    public void changeScene(string name)
    {
        if (scenes.ContainsKey(name))
        {
            if (currentScene is not null)
                currentScene.Hide();
            currentScene = scenes[name];
            currentScene.Show();
        }
        else
        {
            string error = $"Scene {name} non trouvé dans le dictionnaire";
            Debug.WriteLine(error);
            throw new Exception(error);
        }
        Console.WriteLine("Change scene vers " + name);
    }

    public void Update()
    {
        ChangeAspectRatio();
        currentScene?.Update(); // put the ? to signify that we know that it could be null
        debugMagic.Update();
    }
    public void Draw()
    {
        currentScene.Draw();
#if DEBUG
        debugMagic.Draw();
#endif
    }

    public void Close()
    {
        //foreach(KeyValuePair<string, Scene> item in scenes)
        foreach(var item in scenes)
        {
            item.Value.Close();
        }
    }
}