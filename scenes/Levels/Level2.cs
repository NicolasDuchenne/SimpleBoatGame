using System.Numerics;
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
            [42, 0 , 33],
            [0 , 0 , 0 ],
            [0 , 0 , 0 ],
            [0 , 1 , 0 ]
        ]";
        
        LevelCreator levelCreator = new LevelCreator(jsonMatrix);
        gridMap = levelCreator.Create(60, 2);
        Score.Instance.InitScore(5, 2, 1);
        
    }

    public override void Draw()
    {
        base.Draw();
        Raylib.DrawTextEx(GameState.Instance.customFont, $"Press Ctr Left or Alt Right to reduce number of turn for send in Past", new Vector2(200, 80), GameState.Instance.customFont.BaseSize,1, Color.Black);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"Press Shift Right or Shift Left to increase number of turn for send in Past", new Vector2(200, 110), GameState.Instance.customFont.BaseSize,1, Color.Black);
    
    }
}