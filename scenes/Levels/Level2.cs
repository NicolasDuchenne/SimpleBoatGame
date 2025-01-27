using System.Numerics;
using Raylib_cs;
public class Level2:SceneGameplay
{
    private Button tutorialButton;
    private Button tutorialButton2;
    public Level2(string scene_name): base(scene_name)
    {
        maxTimer = 4;
        maxMoves = 2;
        maxSendToPast = 1;
        InitLevelScore();
        tutorialButton = new Button(
            new Rectangle(50, 150, 270, 50),
            $"Press Ctr Left or Alt Right to reduce the\nnumber of turn for send in another dimension",
            Color.White,
            10,
            true);
        tutorialButton2 = new Button(
            new Rectangle(50, 210, 270, 50),
            $"Press Shift Right or Shift Left to increase the\nnumber of turn for send in another dimension",
            Color.White,
            10,
            true);
    }
    public override void Show()
    {
        jsonMatrix = @"
        [
            [41, 0 , 33],
            [0 , 0 , 0 ],
            [0 , 0 , 0 ],
            [0 , 1 , 0 ]
        ]";
        base.Show();    
    }

    public override void Draw()
    {
        base.Draw();
        tutorialButton.Draw();
        tutorialButton2.Draw();
    }
}