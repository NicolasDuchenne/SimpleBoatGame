using System.Numerics;
using Raylib_cs;
public class Level1:SceneGameplay
{
    public Level1(string scene_name): base(scene_name)
    {
        
    }
    public override void Show()
    {
        base.Show();
        string jsonMatrix = @"
        [
            [0 , 0 , 0 , 42],
            [0, 21, 0 , 0],
            [0, 1 , 0 , 0],
            [0, 32, 32, 0]
        ]";
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create();
        
    }
}