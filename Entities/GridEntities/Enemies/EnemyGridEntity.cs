using System.Numerics;
using Raylib_cs;

public class EnemyGridEntity: GridEntity
{
    
    public int TargetColumn {get; protected set;} = GameState.Instance.GridMap.Tiles.Count - 1;
    public int TargetRow {get; protected set;} = GameState.Instance.GridMap.Tiles[0].Count - 1;
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
        if (Direction != new Vector2())
        {
            Direction = direction;
        }
        GameState.Instance.enemyNumber ++;
        if ((Column+1) > GameState.Instance.GridMap.ColumnNumber/2)
        {
            Flip=true;
        }
    }

    private void Shoot()
    {
        if (Direction == directions[0] || Direction == directions[1])
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
        else if (Direction == directions[2] || Direction == directions[3])
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
                bool hasMoved = Move(Direction);
                if ((hasMoved == false) & (InThePast == false) &(touchedPlayer==false))
                {
                    changeDirection = true;
                    Direction = new Vector2(-Direction.X, -Direction.Y);
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