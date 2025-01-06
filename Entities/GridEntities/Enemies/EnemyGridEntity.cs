using System.Numerics;
using Raylib_cs;

public class EnemyGridEntity: GridEntity
{
    float timer = 0;
    
    public int TargetColumn {get; protected set;} = GameState.Instance.GridMap.Tiles.Count - 1;
    public int TargetRow {get; protected set;} = GameState.Instance.GridMap.Tiles[0].Count - 1;
    public Vector2 Direction = new Vector2(0,1);
    public bool changeDirection = false;
    public int shootCounter = 0;
    public int shootTurn = 3;
    public bool willShoot = false;



    public EnemyGridEntity(Sprite sprite, int column, int row, Vector2 direction = new Vector2(), bool canBeSentInThePast=true): base(sprite, column,  row, direction, canBeSentInThePast)
    {
        CanBeMoved = false;
        CanMoveEntities = false;
        CanBeHurt = true;
        CanHurtPlayer = true;
        if (direction != new Vector2())
        {
            Direction = direction;
        }
        GameState.Instance.enemyNumber ++;
    }

    private void Shoot()
    {
        Projectiles.Create(Projectiles.Missile, Column, Row+1,new Vector2(0,1));
        willShoot = false;
    }

    public override void Update()
    {
        if (InThePast == false)
        {
            if ((Position == TargetPosition) & (willShoot))
            {
                Shoot();
                willShoot = false;
            }
            if (Timers.Instance.EnemyPlayTurn)
            {
                shootCounter ++;
                if (shootCounter == shootTurn)
                {
                    shootCounter = 0;
                    willShoot = true;
                }
                bool hasMoved = Move(Direction);
                if ((hasMoved == false) & (InThePast == false) &(touchedPlayer==false))
                {
                    changeDirection = true;
                    Direction.X = -Direction.X;
                    Direction.Y = -Direction.Y;
                    Move(Direction);
                }
                
            }
                
            base.Update();
        }
        
    }

    public override void Destroy()
    {
        GameState.Instance.enemyNumber --;
        base.Destroy();
    }

}