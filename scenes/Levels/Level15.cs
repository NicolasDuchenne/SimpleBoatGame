using System.Numerics;
using Raylib_cs;
public class Level15: SceneGameplay
{
    public Level15(string scene_name): base(scene_name)
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
            [0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 ]
        ]";
        base.Show();       
    }
}