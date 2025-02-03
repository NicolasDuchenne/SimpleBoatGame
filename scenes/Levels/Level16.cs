using System.Numerics;
using Raylib_cs;
public class Level16: SceneGameplay
{
    public Level16(string scene_name): base(scene_name)
    {
        gridMapSize=40;
        gridMapRangeSendInPast = 2;
        maxElemInPast = 2;
        maxTimer = 18;
        maxMoves = 14;
        maxSendToPast = 4;
        InitLevelScore();
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [71, 0 , 0 , 0 , 31, 0 , 0 , 0 , 71],
            [0 , 35, 0 , 0 , 0 , 0 , 0 , 35, 0 ],
            [0 , 31, 0 , 0 , 0 , 0 , 0 , 31, 0 ],
            [0 , 0 , 0 , 0 , 1 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [31, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 31],
            [35, 0 , 31, 0 , 0 , 0 , 31, 0 , 35],
            [0 , 0 , 0 , 71, 31, 71, 0 , 0 , 0 ]
        ]";
        base.Show();       
    }
}