using System.Numerics;
using Raylib_cs;
public class Level3: SceneGameplay
{
    public Level3(string scene_name): base(scene_name)
    {
        
    }
    //needs to be debugged
    public override void Show()
    {
        base.Show();
        GameState.Instance.MaxElemInPast = 2;
        // string jsonMatrix = @"
        // [
        //     [42, 0 , 0 , 41],
        //     [32, 22, 0 , 32],
        //     [32, 1 , 0 , 32],
        //     [32, 32, 32, 32]
        // ]";
        string jsonMatrix = @"
        [
            [61, 0 , 0 , 61],
            [34, 0 , 0 , 34],
            [34, 1 , 0 , 34],
            [34, 34, 34, 34]
        ]";
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create();
        
    }
}