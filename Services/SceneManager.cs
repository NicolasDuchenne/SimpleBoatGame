using System.Diagnostics;
public class SceneManager
{
    private Scene? currentScene;
        private Dictionary<string, Scene> scenes;

    public SceneManager()
    {
        scenes = new Dictionary<string, Scene>();
        Services.Register<SceneManager>(this);
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
        
    }

    

    
    public void Update()
    {
        GameState.Instance.Mouse.Update();
        GameState.Instance.ChangeAspectRatio();
        
        currentScene?.Update(); // put the ? to signify that we know that it could be null
        GameState.Instance.debugMagic.Update();
        GameState.Instance.debugMagic.AddOption("max current level", GameState.Instance.maxCurrentLevel);
    }
    public void Draw()
    {
        currentScene.Draw();
#if DEBUG
        GameState.Instance.debugMagic.Draw();
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

    
    public void RegisterScene(Scene scene, string next_scene="")
    {
        scenes[scene.name] = scene;
        if (next_scene!="")
            scene.next_scene = next_scene;
    }

    public void RemoveScene(string name)
    {
        if (scenes.ContainsKey(name))
        {
            scenes[name].Close();
            scenes.Remove(name);
        }
    }
}

