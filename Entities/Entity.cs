using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Raylib_cs;

public class Entity
{
    public string name {get; protected set;}
    public static List<Entity> ALL = new List<Entity>();
    public Sprite Sprite {get; private set;}
    public Vector2 Position;
    public Vector2 Velocity;
    private Rectangle Box;
    public float Rotation = 0;
    public float Scale = 1;
    public bool Flip = false;
    public Color BaseColor = Color.White;
    public string State = "";
    public string Type = "";
    public bool Visible = true;
    public bool Destroyed {get; protected set;} = false;
    public bool Debug = false;
    public string DebugLabel = "";
    public Entity(Sprite sprite, Vector2 position)
    {
        Sprite = sprite;
        Position = position;
        Velocity = Vector2.Zero;
        Box = new Rectangle(Position.X, Position.Y, Sprite.Width, Sprite.Height);
        ALL.Add(this);
    }

    public virtual void Destroy()
    {
        Destroyed = true;
    }
    

    public virtual void Update()
    {
        if (Destroyed)
            return;

        Sprite.Update();
        DebugLabel = Rotation.ToString();
    }

    public virtual void Draw(Color? color=null)
    {
        if (Destroyed)
            return;
        if (!Visible)
            return;
        if (color is not null)
        {
            Sprite.Draw(Position, Rotation, (Color)color, Flip);
        }
        else
        {
            Sprite.Draw(Position, Rotation, BaseColor, Flip);
        }
#if DEBUG
        if(Debug)
        {
            Raylib.DrawText(DebugLabel, (int)(Position.X+Sprite.Width + 2), (int)Position.Y, 12, Color.Red);
        }
#endif
    }

    public static void UpdateAll()
    {

        for (int i=ALL.Count-1; i>=0; i--)
        {
            ALL[i].Update();
            if (ALL[i].Destroyed)
            {
                ALL.RemoveAt(i);
            }
        }
    }
    public static void DrawAll()
    {
        foreach (Entity entity in ALL)
        {
            entity.Draw();
        }
    }

    public static void ToggleDebug()
    {
        foreach(Entity entity in ALL)
        {
            entity.Debug = !entity.Debug;
        }
    }

}