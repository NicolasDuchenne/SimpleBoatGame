using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Raylib_cs;

public class Destructions
{
    public static Destruction Create(Vector2 position)
    {
        Sprite sprite = new Sprite(Raylib.LoadTexture("ressources/images/Retro Impact Effect Pack ALL/Retro Impact Effect Pack 1 A.png"), 8,24, 12, 64, 64, 72, 79);
        return new Destruction(sprite, position);
    }
}
public class Destruction: Entity
{
    public Destruction(Sprite sprite, Vector2 position): base(sprite, position)
    {

    }
    public override void Update()
    {
        base.Update();
        Console.WriteLine(Sprite.FinishedAnimation);
        if (Sprite.FinishedAnimation)
        {
            Destroy();
        }
            
    }


}