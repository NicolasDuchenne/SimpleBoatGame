using System.Numerics;
using Raylib_cs;
public class UI
{
    public string Name {get; private set;}
    private int powerSize = 50;
    private Power sendToPast ;
    public UI(string name)
    {
        Name = name;
        sendToPast =  new Power(
            new Vector2(100, GameState.Instance.GameScreenHeight-powerSize*0.5f-10),
            powerSize,
            Power.powerSendToAnotherDimension,
            "Send Entity to another Dimension",
            new Vector2(250,50),
            GameState.Instance.MaxElemInPast,
            Color.SkyBlue
            );
    }
    public void Update()
    {
        Timers.Instance.Update();
        sendToPast.Update();
    }
    public void Draw()
    {
        Timers.Instance.Draw();
        Raylib.DrawTextEx(GameState.Instance.customFontBig, $"Level {Name}", new Vector2((int)(GameState.Instance.GameScreenWidth*0.5)-50, 5), GameState.Instance.customFontBig.BaseSize,1, Color.Black);
        sendToPast.Draw();
    }

    

}