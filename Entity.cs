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
    

    public void Update()
    {
        if (Destroyed)
            return;
        Position += Velocity*Raylib.GetFrameTime();
        Box.Position = Position;
        DebugLabel = Velocity.ToString();
        if (Position.X > GameState.Instance.GameScreenHeight)
            Destroy();
    }

    public void Draw()
    {
        if (Destroyed)
            return;
        if (!Visible)
            return;
        Raylib.DrawTextureEx(Texture, Position, Rotation, Scale, BaseColor);
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