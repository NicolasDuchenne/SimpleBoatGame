public abstract class Scene
{
    public static int nombreDeScene;
    public string name;

    public Scene()
    {
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