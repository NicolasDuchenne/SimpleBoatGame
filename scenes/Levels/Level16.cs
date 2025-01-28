using System.Numerics;
using Raylib_cs;
public class Level16: SceneGameplay
{
    public Level16(string scene_name): base(scene_name)
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
            [0 , 0 , 0 , 0 , 41 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 1 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ]
        ]";
        base.Show();       
    }
}