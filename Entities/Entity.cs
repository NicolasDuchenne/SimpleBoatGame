using System.Numerics;
using Raylib_cs;

public class Entity
{
    public static List<Entity> ALL = new List<Entity>();
    public Texture2D Texture {get; private set;}
    public Vector2 Position;
    public Vector2 Velocity;
    private Rectangle Box;
    public float Rotation = 0;
    public float Scale = 1;
    public Color BaseColor = Color.White;
    public string State = "";
    public string Type = "";
    public bool Visible = true;
    public bool Destroyed {get; private set;} = false;
    public bool Debug = false;
    public string DebugLabel = "";
    public Entity(Texture2D texture, Vector2 position)
    {
        Texture = texture;
        Position = position;
        Velocity = Vector2.Zero;
        Box = new Rectangle(Position.X, Position.Y, Texture.Width, Texture.Height);
        ALL.Add(this);
    }

    public void Destroy()
    {
        Destroyed = true;
    }
    

    public virtual void Update()
    {
        if (Destroyed)
            return;
        Position += Velocity*Raylib.GetFrameTime();
        Box.Position = Position;
        DebugLabel = Velocity.ToString();
        if (Position.X > GameState.Instance.GameScreenWidth)
        {
            Destroy();
        }
            
    }

    public static void DrawCentre(Texture2D texture, Vector2 position, float angle, Color Color)
    {
        Rectangle img_rect_source = new Rectangle(0,0, texture.Width, texture.Height);
        Rectangle img_rect_dest = new Rectangle(position.X,position.Y, texture.Width, texture.Height);
        Raylib.DrawTexturePro(
            texture,
            img_rect_source,
            img_rect_dest,
            new Vector2((float)texture.Width/2, (float)texture.Height/2),
            (float)angle,
            Color);
    }

    public void Draw()
    {
        if (Destroyed)
            return;
        if (!Visible)
            return;

        DrawCentre(Texture, Position, Rotation, BaseColor);
#if DEBUG
        if(Debug)
        {
            Raylib.DrawText(DebugLabel, (int)(Position.X+Texture.Width + 2), (int)Position.Y, 12, Color.Red);
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

}