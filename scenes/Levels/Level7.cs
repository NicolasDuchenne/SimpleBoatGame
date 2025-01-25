using System.Numerics;
using Raylib_cs;
public class Level7: SceneGameplay
{
    public Level7(string scene_name): base(scene_name)
    {
        gridMapRangeSendInPast = 2;
        maxTimer = 10;
        maxMoves = 4;
        maxSendToPast = 1;
        InitLevelScore();
    }
    //needs to be debugged
    public override void Show()
    {   
        jsonMatrix = @"
        [
            [62, 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 ],
            [34, 0 , 34, 34, 34, 34],
            [0 , 24, 0 , 0 , 0 , 0 ],
            [0 , 1 , 0 , 0 , 0 , 0 ]
        ]";
        base.Show();
        
    }
}