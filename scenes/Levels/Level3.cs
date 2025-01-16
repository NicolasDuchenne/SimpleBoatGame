using Raylib_cs;
public class Level3:SceneGameplay
{
    public Level3(string scene_name): base(scene_name)
    {
        
    }
    public override void Show()
    {
        jsonMatrix = @"
        [
            [42, 0 , 0 ],
            [0 , 21, 0 ],
            [0 , 0 , 0 ],
            [0 , 1 , 0 ]
        ]";
        Score.Instance.InitScore(8, 4, 1);
        base.Show();
        
    }
}