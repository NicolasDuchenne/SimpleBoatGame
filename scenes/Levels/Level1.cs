using System.Numerics;
using Raylib_cs;
public class Level1:SceneGameplay
{

    public Level1(string scene_name): base(scene_name)
    {
        gridMapSize = 60;
        gridMapRangeSendInPast = 1;
        maxTimer = 4;
        maxMoves = 3;
        maxSendToPast = 1;
        InitLevelScore();
        
    }
    public override void Show()
    {
        jsonMatrix = @"
        [
            [41, 0 , 0, 31],
            [0 , 0 , 0, 0 ],
            [0 , 0 , 0, 0 ],
            [0 , 1 , 0, 0 ]
        ]";
        base.Show();
    }

    public override void Draw()
    {
        base.Draw();
        Raylib.DrawTextEx(GameState.Instance.customFont, $"Left Click on valid entity to send it to the futur when close enough", new Vector2(200, 80), GameState.Instance.customFont.BaseSize,1, Color.Black);
    }
}