using System.Numerics;
using Raylib_cs;
public class Level11: SceneGameplay
{
    public Level11(string scene_name): base(scene_name)
    {
        gridMapSize=40;
        gridMapRangeSendInPast = 2;
        maxElemInPast = 2;
        maxTimer = 13;
        maxMoves = 17;
        maxSendToPast = 4;
        InitLevelScore();
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [32, 56, 31, 32, 0 , 0 , 0 , 0 , 0 ],
            [32, 0 , 0 , 32, 0 , 0 , 0 , 0 , 0 ],
            [32, 0 , 32, 32, 0 , 0 , 0 , 0 , 0 ],
            [32, 0 , 32, 32, 0 , 0 , 0 , 0 , 0 ],
            [32, 0 , 32, 32, 0 , 0 , 0 , 0 , 0 ],
            [32, 0 , 32, 32, 0 , 0 , 0 , 0 , 0 ],
            [32, 0 , 32, 32, 0 , 0 , 0 , 0 , 0 ],
            [32, 0 , 32, 32, 0 , 0 , 0 , 0 , 0 ],
            [32, 0 , 32, 32, 0 , 0 , 0 , 0 , 0 ],
            [32, 0 , 32, 32, 32, 32, 32, 32, 32],
            [32, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 1 ]
        ]";
        base.Show();       
    }
}