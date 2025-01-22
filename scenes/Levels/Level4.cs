using System.Numerics;
using Raylib_cs;
public class Level4: SceneGameplay
{
    public Level4(string scene_name): base(scene_name)
    {
        gridMapRangeSendInPast = 2;

        maxTimer = 5;
        maxMoves = 6;
        maxSendToPast = 2;
        InitLevelScore();
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [41, 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 ],
            [34, 33, 34, 34, 34, 34],
            [0 , 21, 0 , 0 , 0 , 0 ],
            [0 , 1 , 0 , 0 , 0 , 0 ]
        ]";
        base.Show();    
    }
}