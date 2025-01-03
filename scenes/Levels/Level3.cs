using Raylib_cs;
public class Level3: SceneGameplay
{
    //needs to be debugged
    public override void Show()
    {
        base.Show();
        string jsonMatrix = @"
        [
            [42, 0 , 0 , 41],
            [32, 22, 0 , 32],
            [32, 1 , 0 , 32],
            [32, 32, 32, 32]
        ]";
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create();
        
    }
}