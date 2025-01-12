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
            [0 , 0 , 0 , 51],
            [35, 21, 23 , 32],
            [35, 1 , 0 , 32],
            [35, 32, 32, 32]
        ]";
        
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create();
        Score.Instance.InitScore(10, 4, 1);
        
    }
}