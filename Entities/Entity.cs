using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Raylib_cs;

public class Entity
{
    public string name {get; protected set;}
    public static List<Entity> ALL1 = new List<Entity>();
    public static List<Entity> ALL2 = new List<Entity>();
    protected static int entityNumber = 0;
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
    public Entity(Sprite sprite, Vector2 position, int layer = 1)
    {
        name = entityNumber.ToString() + "_";
        Sprite = sprite;
        Position = position;
        Velocity = Vector2.Zero;
        Box = new Rectangle(Position.X, Position.Y, Sprite.Width, Sprite.Height);
        if (layer == 1)
        {
            ALL1.Add(this);
        }
        else
        {
            ALL2.Add(this);
        }
        
        entityNumber++;
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
        UpdateAllLayer(ALL1);
        UpdateAllLayer(ALL2);
    }

    public static void UpdateAllLayer(List<Entity> all)
    {
        for (int i=all.Count-1; i>=0; i--)
        {
            all[i].Update();
            if (all[i].Destroyed)
            {
                all.RemoveAt(i);
            }
        }
    }
    public static void DrawAll()
    {
        DrawAllLayer(ALL1);
        DrawAllLayer(ALL2);
    }
    public static void DrawAllLayer(List<Entity> all)
    {
        foreach (Entity entity in all)
        {
            entity.Draw();
        }
    }

    public static void ToggleDebug()
    {
        foreach(Entity entity in ALL1)
        {
            entity.Debug = !entity.Debug;
        }
        foreach(Entity entity in ALL2)
        {
            entity.Debug = !entity.Debug;
        }
    }
    public static void ClearEntity()
    {
        ALL1.Clear();
        ALL2.Clear();
    }

}