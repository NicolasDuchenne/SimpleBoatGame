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
    public float OneSecondTimer {get; private set;} = 0;
    public bool OneSecondTurn {get; private set;}

    public float PlayerTimer {get; private set;} = 0;
    private float playerTurnDuration = 0.5f;
    public bool PlayerPlayTurn {get; private set;}

    public bool HalfSecondTurn{get; private set;}
    private bool halfSecondFinished;

    private int GameScreenWidth;
    private int GameScreenHeight;

    public Timers()
    {
        GameScreenWidth = GameState.Instance.GameScreenWidth;
        GameScreenHeight = GameState.Instance.GameScreenHeight;
    }

    private void updateTimers()
    {
        OneSecondTurn = false;
        PlayerPlayTurn = false;
        HalfSecondTurn = false;
        
        OneSecondTimer+=Raylib.GetFrameTime();
        PlayerTimer+=Raylib.GetFrameTime();
        GameState.Instance.debugMagic.AddOption("playerTimer", Math.Round(OneSecondTimer,1));
        if (OneSecondTimer > 1)
        {
            OneSecondTimer = 0;
            OneSecondTurn = true;
        } 
        if (PlayerTimer > playerTurnDuration)
        {
            PlayerTimer = 0;
            PlayerPlayTurn = true;
        } 
        if (OneSecondTimer > 0.5)
        {
            if (halfSecondFinished == false)
            {
                halfSecondFinished = true;
                HalfSecondTurn = true;
            }
        }
        else
        {
            halfSecondFinished = false;
        }

        
            
    }

    public void Update()
    {
        updateTimers();
    }

    private void DrawTimer()
    {
        Raylib.DrawRectangleRec(new Rectangle((GameScreenWidth-timerWidth)/2, GameScreenHeight-timerHeight - timerYOffset, (int)Math.Round(timerWidth*OneSecondTimer), timerHeight), Color.White);
        Raylib.DrawRectangleLinesEx(new Rectangle((GameScreenWidth-timerWidth)/2, GameScreenHeight-timerHeight- timerYOffset, timerWidth, timerHeight),2, Color.Black);
        Raylib.DrawLineEx(new Vector2(GameScreenWidth/2, GameScreenHeight-timerHeight - timerYOffset),  new Vector2(GameScreenWidth/2, GameScreenHeight-timerYOffset),2, Color.Black);
    }

    public void Draw()
    {
        DrawTimer();
    }
}