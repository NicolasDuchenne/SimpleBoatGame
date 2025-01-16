using System.Numerics;
using Raylib_cs;
public class Level7: SceneGameplay
{
    public Level7(string scene_name): base(scene_name)
    {
        gridMapRangeSendInPast = 2;
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [52, 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 ],
            [52, 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 0 , 0 , 0 , 0 ],
            [0 , 0 , 24, 0 , 0 , 0 ],
            [0 , 0 , 1 , 0 , 0 , 0 ]
        ]";
        Score.Instance.InitScore(7, 8, 0);
        base.Show();
        GameState.Instance.MaxElemInPast = 1;
        
    }
}