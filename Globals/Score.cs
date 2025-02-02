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

    
    public float MaxTimer {get; private set;}= 10;
    public int MaxMoves {get; private set;}= 4;
    public int MaxSendToPast {get; private set;}= 1;

    private Vector2 scorePos = new Vector2(GameState.Instance.GameScreenWidth*0.5f-100, GameState.Instance.GameScreenHeight*0.30f);

    public Score()
    {
        StarTimer = starFailed;
        StarPast = starFailed;
        StarMoves = starFailed;
    }

    public void ResetScore()
    {
        Timer = 0;
        Moves = 0;
        SendToPast = 0;
    }

    public void UpdateStars()
    {
        if (Math.Round(Timer,1) > MaxTimer)
        {
            StarTimer = starFailed;
            hasTimerStar = false;
        }
        else
        {
            StarTimer = star;
            hasTimerStar = true;
        }
        if (SendToPast >MaxSendToPast)
        {
            StarPast = starFailed;
            hasPastStar = false;
        }
        else
        {
            StarPast = star;
            hasPastStar = true;
        }
        if (Moves >MaxMoves)
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
        MaxTimer = maxTimer;
        MaxMoves = maxMoves;
        MaxSendToPast = maxSendToPast;
    }


    public void Draw()
    {
        Raylib.DrawRectangle((int)scorePos.X-20, (int)scorePos.Y-10, 240, 120, Color.White);
        Raylib.DrawRectangleLines((int)scorePos.X-20, (int)scorePos.Y-10, 240, 120, Color.Black);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"You Won", scorePos + new Vector2(50, 0) , GameState.Instance.customFont.BaseSize,1, Color.Black);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"timer: {Math.Round(Timer,2)}/{MaxTimer}", scorePos + new Vector2(0, 1*(GameState.Instance.customFont.BaseSize+5)), GameState.Instance.customFont.BaseSize,1, Color.Black);
        StarTimer.Draw(scorePos + new Vector2(200, 1*(GameState.Instance.customFont.BaseSize+5)), 0, Color.White, false);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"moves: {Moves}/{MaxMoves}", scorePos +new Vector2(0, 2*(GameState.Instance.customFont.BaseSize+5)), GameState.Instance.customFont.BaseSize,1, Color.Black);
        StarMoves.Draw(scorePos +new Vector2(200, 2*(GameState.Instance.customFont.BaseSize+5)), 0, Color.White, false);
        Raylib.DrawTextEx(GameState.Instance.customFont, $"power use: {SendToPast}/{MaxSendToPast}",scorePos +new Vector2(0, 3*(GameState.Instance.customFont.BaseSize+5)), GameState.Instance.customFont.BaseSize,1, Color.Black);
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
        float meanScoreTimer = Timer/MaxTimer;
        float meanScoreMoves = Moves/MaxMoves;
        float meanScorePast = SendToPast/MaxSendToPast;
        float meanScore  = (meanScoreTimer + meanScoreMoves + meanScorePast)/3;
        return meanScore;
    }

    private int ComputeNumberOfStars()
    {
        int numberOfStars = 0;
        numberOfStars += hasTimerStar ? 1 : 0;
        numberOfStars += hasMovesStar ? 1 : 0;
        numberOfStars += hasPastStar ? 1 : 0;
        return numberOfStars;
    }

    public void updateBestScore(Score score)
    {
        int scoreNumberOfStars = score.ComputeNumberOfStars();
        int numberOfStars = ComputeNumberOfStars();
        bool hasMoreStars = scoreNumberOfStars>numberOfStars;
        bool hasSameNumberOfStars =  scoreNumberOfStars==numberOfStars;
        // Replace best score if you get more star of if you get the same number of star with a better mean score
        if (hasMoreStars || (hasSameNumberOfStars & score.ComputeMeanScore() < ComputeMeanScore()))
        {
            Timer = score.Timer;
            hasTimerStar = score.hasTimerStar;       
            Moves = score.Moves;
            hasMovesStar = score.hasMovesStar;
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