using Raylib_cs;
public class Level3:SceneGameplay
{
    public Level3(string scene_name): base(scene_name)
    {
        maxTimer = 6;
        maxMoves = 2;
        maxSendToPast = 1;
        InitLevelScore();
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
        base.Show();    
    }
}