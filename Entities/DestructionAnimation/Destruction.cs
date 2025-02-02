using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Raylib_cs;

public class DestructionAnimations
{
    private static Texture2D destructionTexture = Raylib.LoadTexture("ressources/images/Retro Impact Effect Pack ALL/Retro Impact Effect Pack 1 A.png");
    public static void Create(Vector2 position)
    {
        Sprite sprite = new Sprite(destructionTexture, null, 8,24, 12, 64, 64, 72, 79);
        new DestructionAnimation(sprite, position);
    }
}
public class DestructionAnimation: Entity
{
    
    public DestructionAnimation(Sprite sprite, Vector2 position): base(sprite, position)
    {
        name = "destruction";
        Sounds.explosionSound.Play(0.8f);
    }
    public override void Update()
    {
        base.Update();
        if (Sprite.FinishedAnimation)
        {
            Destroy();
        }
            
    }


}