using System.Numerics;
using Raylib_cs;
public class SceneLevel1:SceneGameplay
{
    public override void Show()
    {
        base.Show();
        string jsonMatrix = @"
        [
            [0 , 0 , 42],
            [32, 21, 32],
            [32, 1 , 32],
            [32, 32, 32]
        ]";
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create();
        
    }
}