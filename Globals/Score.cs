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

    
    private float maxTimer = 10;
    private int maxMoves = 4;
    private int maxSendToPast = 1;

    private Vector2 scorePos = new Vector2(GameState.Instance.GameScreenWidth*0.5f-100, GameState.Instance.GameScreenHeight*0.30f);

    public Score()
    {
    }

    public void ResetScore()
    {
        Timer = 0;
        Moves = 0;
        SendToPast = 0;
    }

    public void UpdateStars()
    {
        if (Timer >Instance.maxTimer)
        {
            StarTimer = starFailed;
            hasTimerStar = false;
        }
        else
        {
            StarTimer = star;
            hasTimerStar = true;
        }
        if (SendToPast >Instance.maxSendToPast)
        {
            StarPast = starFailed;
            hasPastStar = false;
        }
        else
        {
            StarPast = star;
            hasPastStar = true;
        }
        if (Moves >Instance.maxMoves)
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

    public void InitScore(float maxTimer, int maxMoves, int maxSendToPast)
    {
        
        this.maxTimer = maxTimer;
        this.maxMoves = maxMoves;
        this.maxSendToPast = maxSendToPast;
    }


    public void Draw()
    {
        Raylib.DrawRectangle((int)scorePos.X-20, (int)scorePos.Y-10, 240, 120, Color.White);
        Raylib.DrawRectangleLines((int)scorePos.X-20, (int)scorePos.Y-10, 240, 120, Color.Black);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"You Won", scorePos + new Vector2(50, 0) , GameState.Instance.customFont.BaseSize,1, Color.Black);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"timer: {Math.Round(Timer,1)}/{Instance.maxTimer}", scorePos + new Vector2(0, 1*(GameState.Instance.customFont.BaseSize+5)), GameState.Instance.customFont.BaseSize,1, Color.Black);
        StarTimer.Draw(scorePos + new Vector2(200, 1*(GameState.Instance.customFont.BaseSize+5)), 0, Color.White, false);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"moves: {Moves}/{Instance.maxMoves}", scorePos +new Vector2(0, 2*(GameState.Instance.customFont.BaseSize+5)), GameState.Instance.customFont.BaseSize,1, Color.Black);
        StarMoves.Draw(scorePos +new Vector2(200, 2*(GameState.Instance.customFont.BaseSize+5)), 0, Color.White, false);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"power use: {SendToPast}/{Instance.maxSendToPast}",scorePos +new Vector2(0, 3*(GameState.Instance.customFont.BaseSize+5)), GameState.Instance.customFont.BaseSize,1, Color.Black);
        StarPast.Draw(scorePos +new Vector2(200, 3*(GameState.Instance.customFont.BaseSize+5)), 0, Color.White, false);
    }

    public void addMove()
    {
        Moves ++;
    }

    public void addSendToPast()
    {
        SendToPast ++;
    }
    private float ComputeMeanScore()
    {
        float meanScoreTimer = Timer/Instance.maxTimer;
        float meanScoreMoves = Moves/Instance.maxMoves;
        float meanScorePast = SendToPast/Instance.maxSendToPast;
        float meanScore  = (meanScoreTimer + meanScoreMoves + meanScorePast)/3;
        return meanScore;
    }

    public void updateBestScore()
    {
        if (Instance.ComputeMeanScore() < ComputeMeanScore())
        {
            Timer = Instance.Timer;
            hasTimerStar = Instance.hasTimerStar;       
            Moves = Instance.Moves;
            hasMovesStar = Instance.hasMovesStar;
            SendToPast = Instance.SendToPast;
            hasPastStar = Instance.hasPastStar;
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