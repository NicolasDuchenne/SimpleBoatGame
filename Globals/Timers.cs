using System.Numerics;
using Raylib_cs;

public class Timers
{

    private static Timers? instance;
    public static Timers Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Timers();
            }
            return instance;
        }
    }


    private int timerWidth = 400;
    private int timerHeight = 20;
    private int timerYOffset = 30;
    public float PlayerTimer {get; private set;} = 0;
    private float playerTurnDuration = 1f;
    public bool PlayerPlayTurn {get; private set;}

    public bool EnemyPlayTurn{get; private set;}
    private bool enemyHasPlayed;
    public float EnemyPlayTime {get; private set;}= 0.5f;

    private int GameScreenWidth;
    private int GameScreenHeight;

    public Timers()
    {
        GameScreenWidth = GameState.Instance.GameScreenWidth;
        GameScreenHeight = GameState.Instance.GameScreenHeight;
    }

    private void updateTimers()
    {
        PlayerPlayTurn = false;
        EnemyPlayTurn = false;
        PlayerTimer+=Raylib.GetFrameTime();
        GameState.Instance.debugMagic.AddOption("playerTimer", Math.Round(PlayerTimer,1));
        if (PlayerTimer > playerTurnDuration)
        {
            PlayerTimer = 0;
            PlayerPlayTurn = true;
        } 
        if (PlayerTimer > playerTurnDuration*EnemyPlayTime)
        {
            if (enemyHasPlayed == false)
            {
                enemyHasPlayed = true;
                EnemyPlayTurn = true;
            }
        }
        else
        {
            enemyHasPlayed = false;
        }

        
            
    }

    public void Update()
    {
        updateTimers();
    }

    private void DrawTimer()
    {
        Raylib.DrawRectangleRec(new Rectangle((GameScreenWidth-timerWidth)/2, GameScreenHeight-timerHeight - timerYOffset, (int)Math.Round(timerWidth*(PlayerTimer/playerTurnDuration)), timerHeight), Color.White);
        Raylib.DrawRectangleLinesEx(new Rectangle((GameScreenWidth-timerWidth)/2, GameScreenHeight-timerHeight- timerYOffset, timerWidth, timerHeight),2, Color.Black);
        Raylib.DrawLineEx(new Vector2(GameScreenWidth/2, GameScreenHeight-timerHeight - timerYOffset),  new Vector2(GameScreenWidth/2, GameScreenHeight-timerYOffset),2, Color.Black);
    }

    public void Draw()
    {
        DrawTimer();
    }
}