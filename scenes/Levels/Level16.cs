using System.Numerics;
using Raylib_cs;
public class Level16: SceneGameplay
{
    public Level16(string scene_name): base(scene_name)
    {
        gridMapSize=40;
        gridMapRangeSendInPast = 3;
        maxElemInPast = 3;
        maxTimer = 28;
        maxMoves = 12;
        maxSendToPast = 17;
        InitLevelScore();
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [41, 31, 31, 31, 41, 31, 31, 31, 41],
            [31, 31, 31, 31, 31, 31, 31, 31, 31],
            [31, 31, 31, 31, 31, 31, 31, 31, 31],
            [31, 31, 31, 31, 31, 31, 31, 31, 31],
            [41, 31, 31, 31, 1 , 31, 31, 31, 41],
            [31, 31, 31, 31, 31, 31, 31, 31, 31],
            [31, 31, 31, 31, 31, 31, 31, 31, 31],
            [31, 31, 31, 31, 31, 31, 31, 31, 31],
            [41, 31, 31, 31, 41, 31, 31, 31, 41]
        ]";
        base.Show();       
    }
}