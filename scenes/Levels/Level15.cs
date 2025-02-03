using System.Numerics;
using Raylib_cs;
public class Level15: SceneGameplay
{
    public Level15(string scene_name): base(scene_name)
    {
        gridMapSize=40;
        gridMapRangeSendInPast = 2;
        maxElemInPast = 2;
        maxTimer = 15;
        maxMoves = 7;
        maxSendToPast = 2;
        InitLevelScore();
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [32, 61, 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [63, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 1 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 23, 0 , 0 , 0 , 63],
            [0 , 0 , 0 , 0 , 0 , 0 , 0 , 61, 32]
        ]";
        base.Show();       
    }
}