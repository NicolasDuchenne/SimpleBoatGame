using Raylib_cs;
public class Level3:SceneGameplay
{
    public Level3(string scene_name): base(scene_name)
    {
        
    }
    public override void Show()
    {
        base.Show();
        string jsonMatrix = @"
        [
            [42, 0 , 0 ],
            [0 , 21, 0 ],
            [0 , 0 , 0 ],
            [0 , 1 , 0 ]
        ]";
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create();
        
    }
}