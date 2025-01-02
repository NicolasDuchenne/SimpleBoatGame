using Raylib_cs;
public class SceneLevel2:SceneGameplay
{
    public override void Show()
    {
        base.Show();
        string jsonMatrix = @"
        [
            [0 , 0 , 41],
            [32, 22, 32],
            [32, 1 , 32],
            [32, 32, 32]
        ]";
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create();
        
    }
}