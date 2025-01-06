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
            [0 , 0 , 0 , 41],
            [32, 22, 0 , 32],
            [32, 1 , 0 , 32],
            [32, 32, 32, 32]
        ]";
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create();
        
    }
}