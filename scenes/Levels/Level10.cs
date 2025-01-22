using System.Numerics;
using Raylib_cs;
public class Level10: SceneGameplay
{
    public Level10(string scene_name): base(scene_name)
    {
        gridMapRangeSendInPast = 2;
        maxTimer = 15;
        maxMoves = 10;
        maxSendToPast = 2;
        InitLevelScore();
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [0 , 0 , 51 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 51 ],
            [0 , 0 , 0 , 41 , 0 , 0 ],
            [0 , 24, 0 , 0 , 0 , 0 ],
            [0 , 1 , 0 , 0 , 0 , 0 ]
        ]";
        base.Show();   
    }
}