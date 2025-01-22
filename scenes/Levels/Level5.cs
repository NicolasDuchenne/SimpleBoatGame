using System.Numerics;
using Raylib_cs;
public class Level5: SceneGameplay
{
    public Level5(string scene_name): base(scene_name)
    {
        gridMapRangeSendInPast = 1;
        maxElemInPast = 2;
        maxTimer = 6;
        maxMoves = 6;
        maxSendToPast = 2;
        InitLevelScore();
        
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
        base.Show();
        
    }
}