using System.Numerics;
using Raylib_cs;
public class Level12: SceneGameplay
{
    public Level12(string scene_name): base(scene_name)
    {
        gridMapSize=40;
        gridMapRangeSendInPast = 2;
        maxElemInPast = 2;
        maxTimer = 10;
        maxMoves = 6;
        maxSendToPast = 2;
        InitLevelScore();
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [51, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 31, 31, 0 , 0 , 0 , 0 ],
            [0 , 31, 23, 23, 23, 23, 23, 31, 0 ],
            [0 , 31, 23, 23, 23, 23, 23, 31, 0 ],
            [0 , 31, 23, 23, 1 , 23, 23, 31, 0 ],
            [0 , 31, 23, 23, 23, 23, 23, 31, 0 ],
            [0 , 31, 23, 23, 23, 23, 23, 31, 0 ]
        ]";
        base.Show();       
    }
}