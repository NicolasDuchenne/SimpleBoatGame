using System.Numerics;
using Raylib_cs;
public class Level12: SceneGameplay
{
    public Level12(string scene_name): base(scene_name)
    {
        gridMapSize=40;
        gridMapRangeSendInPast = 2;
        maxElemInPast = 2;
        maxTimer = 25;
        maxMoves = 8;
        maxSendToPast = 3;
        InitLevelScore();
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [0, 51 , 0 , 0 , 0 , 0 , 0],
            [0 , 0 , 0 , 0 , 0 , 0, 0 ],
            [51 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 32, 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 24, 32, 0 ],
            [0 , 0 , 0 , 0 , 31, 0 , 0 ],
            [0 , 0 , 0 , 1 , 0 , 24, 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 ]
        ]";
        base.Show();       
    }
}