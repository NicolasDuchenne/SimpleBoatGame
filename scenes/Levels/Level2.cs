using Raylib_cs;
public class Level2:SceneGameplay
{
    public Level2(string scene_name): base(scene_name)
    {
        
    }
    public override void Show()
    {
        base.Show();
        string jsonMatrix = @"
        [
            [0 , 0 , 0 , 42],
            [35, 21, 0 , 33],
            [35, 1 , 0 , 33],
            [33, 33, 33, 33]
        ]";
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create();
        
    }
}