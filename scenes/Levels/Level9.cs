using System.Numerics;
using Raylib_cs;
public class Level9: SceneGameplay
{
    public Level9(string scene_name): base(scene_name)
    {
        gridMapRangeSendInPast = 2;
        maxTimer = 8;
        maxMoves = 7;
        maxSendToPast = 0;
        InitLevelScore();
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [52, 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 51],
            [0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 23, 0 , 0 , 0 , 0 ],
            [0 , 1 , 0 , 0 , 0 , 0 ]
        ]";
        base.Show();
    }
}