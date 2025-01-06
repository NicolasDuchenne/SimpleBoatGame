using System.Numerics;
using Raylib_cs;
public class UI
{
    public string Name {get; private set;}
    public UI(string name)
    {
        Name = name;
    }
    public void Update()
    {
        Timers.Instance.Update();
    }
    public void Draw()
    {
        Timers.Instance.Draw();
        Raylib.DrawTextEx(GameState.Instance.customFontBig, $"Level {Name}", new Vector2((int)(GameState.Instance.GameScreenWidth*0.5)-50, 5), GameState.Instance.customFontBig.BaseSize,1, Color.Black);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"Elem sent in past {GameState.Instance.elemInPast}/{GameState.Instance.MaxElemInPast}", new Vector2(GameState.Instance.GameScreenWidth-300, 10),  GameState.Instance.customFont.BaseSize,1, Color.Black);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"Send Elem {GameState.Instance.MaxTurnInPast} turn in the past", new Vector2(GameState.Instance.GameScreenWidth-300, 40), GameState.Instance.customFont.BaseSize,1, Color.Black);
    }

    

}