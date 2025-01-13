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
    private float maxTimer = 10;
    private int maxMoves =4;
    private int maxSendToPast= 1;

    public float Timer {get; private set;}= float.PositiveInfinity;
    public int Moves {get; private set;}= int.MaxValue;
    public int SendToPast {get; private set;}= int.MaxValue;

    private bool hasTimerStar = false;
    private bool hasMovesStar = false;
    private bool hasPastStar = false;

    private Sprite star = new Sprite(Raylib.LoadTexture("ressources/images/png/star.png"));
    private Sprite starFailed = new Sprite(Raylib.LoadTexture("ressources/images/png/starFailed.png"));
    public Sprite StarTimer {get; private set;}
    public Sprite StarMoves {get; private set;}
    public Sprite StarPast {get; private set;}

    private Vector2 scorePos = new Vector2(GameState.Instance.GameScreenWidth*0.5f-100, GameState.Instance.GameScreenHeight*0.25f);

    public void InitScore(float maxTimer, int maxMoves, int maxSendToPast)
    {
        this.maxTimer = maxTimer;
        this.maxMoves = maxMoves;
        this.maxSendToPast = maxSendToPast;
    }
    public void ResetScore()
    {
        Timer = 0;
        Moves = 0;
        SendToPast = 0;
    }

    public void UpdateStars()
    {
        if (Timer >maxTimer)
        {
            StarTimer = starFailed;
            hasTimerStar = false;
        }
        else
        {
            StarTimer = star;
            hasTimerStar = true;
        }
        if (SendToPast >maxSendToPast)
        {
            StarPast = starFailed;
            hasPastStar = false;
        }
        else
        {
            StarPast = star;
            hasPastStar = true;
        }
        if (Moves >maxMoves)
        {
            StarMoves = starFailed;
            hasMovesStar = false;
        }
        else
        {
            StarMoves = star;
            hasMovesStar = true;
        }
    }
    
    public void Update()
    {
        Timer +=Raylib.GetFrameTime();
        UpdateStars();
        
    }

    public void Draw()
    {
        Raylib.DrawRectangle((int)scorePos.X-20, (int)scorePos.Y-20, 240, 100, Color.White);
        Raylib.DrawRectangleLines((int)scorePos.X-20, (int)scorePos.Y-20, 240, 100, Color.Black);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"timer: {Math.Round(Timer,1)}/{maxTimer}", scorePos, GameState.Instance.customFont.BaseSize,1, Color.Black);
        StarTimer.Draw(scorePos + new Vector2(200, 0), 0, Color.White, false);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"moves: {Moves}/{maxMoves}", scorePos +new Vector2(0, GameState.Instance.customFont.BaseSize+5), GameState.Instance.customFont.BaseSize,1, Color.Black);
        StarMoves.Draw(scorePos +new Vector2(200, GameState.Instance.customFont.BaseSize+5), 0, Color.White, false);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"power use: {SendToPast}/{maxSendToPast}",scorePos +new Vector2(0, 2*(GameState.Instance.customFont.BaseSize+5)), GameState.Instance.customFont.BaseSize,1, Color.Black);
        StarPast.Draw(scorePos +new Vector2(200, 2*(GameState.Instance.customFont.BaseSize+5)), 0, Color.White, false);
    }

    public void addMove()
    {
        Moves ++;
    }

    public void addSendToPast()
    {
        SendToPast ++;
    }

    public void updateBestScore(Score score)
    {
        if (score.Timer < Timer)
        {
            Timer = score.Timer;
            hasTimerStar = score.hasTimerStar;
        }
        if (score.Moves < Moves)
        {
            Moves = score.Moves;
            hasMovesStar = score.hasMovesStar;
        }
        if (score.SendToPast < SendToPast)
        {
            SendToPast = score.SendToPast;
            hasPastStar = score.hasPastStar;
        }
        UpdateStars();
    }

    public void SetDefaultScore()
    {
        Timer= float.PositiveInfinity;
        Moves= int.MaxValue;
        SendToPast= int.MaxValue;
        UpdateStars();
    }

    public void SetScore(float timer, int moves, int sendToPast)
    {
        Timer = timer;
        Moves = moves;
        SendToPast = sendToPast;
        UpdateStars();
    }
}