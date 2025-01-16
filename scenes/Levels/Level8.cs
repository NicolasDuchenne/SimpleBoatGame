using System.Numerics;
using Raylib_cs;
public class Level8: SceneGameplay
{
    public Level8(string scene_name): base(scene_name)
    {
        gridMapRangeSendInPast = 2;
        
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
        Score.Instance.InitScore(10, 4, 1);
        base.Show();
        GameState.Instance.MaxElemInPast = 1;
        
    }
}