using Raylib_cs;
public class Level3:SceneGameplay
{
    private Button tutorialButton;
    public Level3(string scene_name): base(scene_name)
    {
        maxTimer = 6;
        maxMoves = 2;
        maxSendToPast = 1;
        InitLevelScore();
        tutorialButton = new Button(
            new Rectangle(50, 200, 250, 50),
            $"Darker Entities cannot be sent to\nanother dimension",
            Color.White,
            10,
            true);
    }
    public override void Show()
    {
        jsonMatrix = @"
        [
            [42, 0 , 0 ],
            [0 , 21, 0 ],
            [0 , 0 , 0 ],
            [0 , 1 , 0 ]
        ]";
        base.Show();    
    }
    public override void Draw()
    {
        base.Draw();
        tutorialButton.Draw();
    }
}