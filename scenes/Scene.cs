public abstract class Scene
{
    public static int nombreDeScene;
    public string name;
    public string next_scene;

    public Scene(string scene_name)
    {
        name = scene_name;
        nombreDeScene++;
    }
    public virtual void Show()
    {

    }

    public virtual void Hide()
    {

    }
    public virtual void Update()
    {
        
    }

    public virtual void Draw()
    {

    }

    public virtual void Close()
    {
        
    }
}