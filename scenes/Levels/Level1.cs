using System.Numerics;
using Raylib_cs;
public class Level1:SceneGameplay
{
    private Button tutorialButton;
    public Level1(string scene_name): base(scene_name)
    {
        gridMapSize = 60;
        gridMapRangeSendInPast = 1;
        maxTimer = 4;
        maxMoves = 3;
        maxSendToPast = 1;
        InitLevelScore();
        tutorialButton = new Button(
            new Rectangle(50, 200, 250, 100),
            $"When close enough, left Click on any valid\nentity to send it to another dimension\n\nWhen an entity comes back it will destroy\nany enemy present at it's location ",
            Color.White,
            10,
            true);
   
        
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
        tutorialButton.Draw();
    }
}