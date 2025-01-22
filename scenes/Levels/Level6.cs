using System.Numerics;
using Raylib_cs;
public class Level6: SceneGameplay
{
    public Level6(string scene_name): base(scene_name)
    {
        gridMapRangeSendInPast = 2;

        maxTimer = 9;
        maxMoves = 7;
        maxSendToPast = 2;
        InitLevelScore();
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [51, 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 ],
            [52, 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 22, 0 , 0 , 0 ],
            [0 , 0 , 1 , 0 , 0 , 0 ]
        ]";
        base.Show();
        
    }
}