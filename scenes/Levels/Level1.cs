using System.Numerics;
using Raylib_cs;
public class Level1:SceneGameplay
{
    public override void Show()
    {
        base.Show();
        string jsonMatrix = @"
        [
            [0 , 0 , 0 , 42],
            [32, 21, 0 , 32],
            [32, 1 , 0 , 32],
            [32, 32, 32, 32]
        ]";
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create();
        
    }
}