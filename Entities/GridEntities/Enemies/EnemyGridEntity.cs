using System.Numerics;
using Raylib_cs;

public class EnemyGridEntity: GridEntity
{
    
    public int TargetColumn {get; protected set;} = GameState.Instance.GridMap.Tiles.Count - 1;
    public int TargetRow {get; protected set;} = GameState.Instance.GridMap.Tiles[0].Count - 1;
    public Vector2 movingDirection = new Vector2(0,1);
    public bool changeDirection = false;
    public int shootCounter = 0;
    public int shootTurn = 3;
    public bool willShoot = false;
    private Vector2 shootingDirection = new Vector2(0,1);
    private int ShootingColumn = 0;
    private int ShootingRow = 0;



    public EnemyGridEntity(Sprite sprite, int column, int row, Vector2 direction = new Vector2(), bool canBeSentInThePast=true): base(sprite, column,  row, direction, canBeSentInThePast)
    {
        name = "enemy";
        CanBeMoved = false;
        CanMoveEntities = false;
        CanBeHurt = true;
        CanHurtPlayer = true;
        if (movingDirection != new Vector2())
        {
            movingDirection = direction;
        }
        GameState.Instance.enemyNumber ++;
        if ((Column+1) > GameState.Instance.GridMap.ColumnNumber/2)
        {
            Flip=true;
        }
    }

    private void Shoot()
    {
        if (movingDirection == directions[0] || movingDirection == directions[1])
        {
            ShootingColumn = Column;
            if ((Row+1) <=GameState.Instance.GridMap.RowNumber/2)
            {
                shootingDirection = new Vector2(0,1);
                ShootingRow = Row + 1;
            }
            else
            {
                shootingDirection = new Vector2(0,-1);
                ShootingRow = Row - 1;
            }
        }
        else if (movingDirection == directions[2] || movingDirection == directions[3])
        {
            ShootingRow = Row;
            if ((Column+1) <=GameState.Instance.GridMap.ColumnNumber/2)
            {
                shootingDirection = new Vector2(1,0);
                ShootingColumn = Column+1;
            }
            else
            {
                shootingDirection = new Vector2(-1,0);
                ShootingColumn = Column-1;
            }
        }
        if (ShootingColumn < GameState.Instance.GridMap.ColumnNumber & ShootingRow < GameState.Instance.GridMap.RowNumber)
        {
            Projectiles.Create(Projectiles.Missile, ShootingColumn, ShootingRow, shootingDirection);
        }
        
        
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
            if (Timers.Instance.HalfSecondTurn)
            {
                shootCounter ++;
                if (shootCounter == shootTurn)
                {
                    shootCounter = 0;
                    willShoot = true;
                }
                bool hasMoved = Move(movingDirection);
                if ((hasMoved == false) & (InThePast == false) &(touchedPlayer==false))
                {
                    changeDirection = true;
                    if (movingDirection.X!=0)
                    {
                        movingDirection.X = -movingDirection.X;
                    }
                       
                    if (movingDirection.Y!=0)
                    {
                        movingDirection.Y = -movingDirection.Y;
                    }
                        

                    Move(movingDirection);
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