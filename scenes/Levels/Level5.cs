using System.Numerics;
using Raylib_cs;
public class Level5: SceneGameplay
{
    private Button tutorialButton;
    public Level5(string scene_name): base(scene_name)
    {
        gridMapRangeSendInPast = 2;
        maxElemInPast = 2;
        maxTimer = 6;
        maxMoves = 6;
        maxSendToPast = 2;
        InitLevelScore();
        tutorialButton = new Button(
            new Rectangle(50, 200, 250, 50),
            $"On some levels, you can send more than\n1 entity in the futur at a time",
            Color.White,
            10,
            true);
        
    }
    //needs to be debugged
    public override void Show()
    {
        
        jsonMatrix = @"
        [
            [31, 72, 31],
            [0 , 0 , 0 ],
            [31, 0 , 31],
            [31, 0 , 31],
            [0 , 0 , 0 ],
            [32, 0 , 32],
            [32, 1 , 32]
        ]";
        base.Show();
        
    }

    public override void Draw()
    {
        base.Draw();
        tutorialButton.Draw();
    }
}