using System.Numerics;
using Raylib_cs;
public class Level5: SceneGameplay
{
    public Level5(string scene_name): base(scene_name)
    {
        gridMapRangeSendInPast = 1;
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [32, 72, 31],
            [32, 0 , 0],
            [32, 0 , 32],
            [32, 0 , 32],
            [32, 0 , 32],
            [32, 0 , 32],
            [32, 1 , 32]
        ]";
        Score.Instance.InitScore(6, 6, 2);
        base.Show();
        GameState.Instance.MaxElemInPast = 2;
        
    }
}