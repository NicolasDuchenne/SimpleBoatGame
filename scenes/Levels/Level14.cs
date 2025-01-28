using System.Numerics;
using Raylib_cs;
public class Level14: SceneGameplay
{
    public Level14(string scene_name): base(scene_name)
    {
        gridMapSize=40;
        gridMapRangeSendInPast = 2;
        maxElemInPast = 2;
        maxTimer = 30;
        maxMoves = 20;
        maxSendToPast = 3;
        InitLevelScore();
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 33, 41],
            [0 , 73, 0 , 0 , 0 , 0 , 0 , 33, 32],
            [0 , 0 , 0 , 1 , 0 , 0 , 0 , 33, 41],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 33, 32],
            [0 , 0 , 21, 0 , 0 , 0 , 0 , 33, 41],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 33, 32],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 33, 41]
        ]";
        base.Show();       
    }
}