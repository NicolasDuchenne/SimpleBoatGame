public interface ISceneManagerService
{
    public void Load<T>() where T : SceneService, new();
}

public class SceneManagerService: ISceneManagerService
{
    private SceneService _currentScene;
    public SceneManagerService()
    {
        Services.Register<ISceneManagerService>(this); // This is so that the service locator can only launch load
    }
    public void Load<T>() where T : SceneService, new()
    {
        //if(_currentScene != null) _currentScene.Unload(); //Equivalent a la ligne en dessous
        _currentScene?.Unload();
        _currentScene = new T();
        _currentScene.Load();

    }
    public void Update()
    {
        _currentScene?.Update();
    }
    public void Draw() => _currentScene?.Draw();
}