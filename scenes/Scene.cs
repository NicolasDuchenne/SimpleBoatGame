public abstract class Scene
{
    public static int nombreDeScene;
    public string name;
    public string next_scene;
    Water water = new Water();
    Musics music = new Musics(Musics.ambianceSeaMusic);

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
        water.Update();
        music.Update();
    }

    public virtual void Draw()
    {
        water.Draw();
    }

    public virtual void Close()
    {
        
    }
}