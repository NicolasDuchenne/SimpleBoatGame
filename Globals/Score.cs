using System.Numerics;
using Raylib_cs;
public class Score
{

    private static Score? instance;
    public static Score Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Score();
            }
            return instance;
        }
    }
    private float maxTimer;
    private int maxMoves;
    private int maxSendToPast;

    private float timer = 0;
    private int moves=0;
    private int sendToPast = 0;
    public void InitScore(float maxTimer, int maxMoves, int maxSendToPast)
    {
        this.maxTimer = maxTimer;
        this.maxMoves = maxMoves;
        this.maxSendToPast = maxSendToPast;
        
    }
    public void ResetScore()
    {
        timer = 0;
        moves = 0;
        sendToPast = 0;
    }
    
    public void Update()
    {
        timer +=Raylib.GetFrameTime();
    }

    public void Draw()
    {
        Raylib.DrawTextEx(GameState.Instance.customFont, $"timer: {Math.Round(timer,1)}", new Vector2(20, GameState.Instance.GameScreenHeight*0.5f), GameState.Instance.customFont.BaseSize,1, Color.Black);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"moves: {moves}", new Vector2(20, GameState.Instance.GameScreenHeight*0.5f + GameState.Instance.customFont.BaseSize+5), GameState.Instance.customFont.BaseSize,1, Color.Black);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"power use: {sendToPast}", new Vector2(20, GameState.Instance.GameScreenHeight*0.5f+ 2*(GameState.Instance.customFont.BaseSize+5)), GameState.Instance.customFont.BaseSize,1, Color.Black);
    }

    public void addMove()
    {
        moves ++;
    }

    public void addSendToPast()
    {
        sendToPast ++;
    }
}