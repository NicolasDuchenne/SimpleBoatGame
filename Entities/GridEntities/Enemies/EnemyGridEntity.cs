using System.Numerics;
using Raylib_cs;

public class EnemyGridEntity: GridEntity
{
    
    public int TargetColumn {get; protected set;} = GameState.Instance.GridMap.Tiles.Count - 1;
    public int TargetRow {get; protected set;} = GameState.Instance.GridMap.Tiles[0].Count - 1;
    public bool changeDirection = false;
    public int shootCounter = 0;
    public int shootTurn;
    public bool willShoot = false;
    private Vector2 shootingDirection = new Vector2(0,1);
    private int ShootingColumn = 0;
    private int ShootingRow = 0;



    public EnemyGridEntity(Sprite sprite, int column, int row, Vector2 direction = new Vector2(), bool canBeSentInThePast=true, int shootTurn = 0, int shootCounter = 0): base(sprite, column,  row, direction, canBeSentInThePast)
    {
        name += "enemy";
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
        this.shootTurn = shootTurn;
        this.shootCounter = shootCounter;
    }

    private void Shoot()
    {
        Vector2 shootingPosition = Position;
        if (Direction == directions[0] || Direction == directions[1])
        {
            ShootingColumn = Column;
            if ((Row+1) <=GameState.Instance.GridMap.RowNumber/2)
            {
                shootingDirection = new Vector2(0,1);
                ShootingRow = Row + 1;
                shootingPosition.Y = shootingPosition.Y + Sprite.Height;
            }
            else
            {
                shootingDirection = new Vector2(0,-1);
                ShootingRow = Row - 1;
                shootingPosition.Y = shootingPosition.Y - Sprite.Height;
            }
        }
        else if (Direction == directions[2] || Direction == directions[3])
        {
            ShootingRow = Row;
            if ((Column+1) <=GameState.Instance.GridMap.ColumnNumber/2)
            {
                shootingDirection = new Vector2(1,0);
                ShootingColumn = Column+1;
                shootingPosition.X = shootingPosition.X + Sprite.Width;
            }
            else
            {
                shootingDirection = new Vector2(-1,0);
                ShootingColumn = Column-1;
                shootingPosition.X = shootingPosition.X - Sprite.Width;
            }
        }
        if (ShootingColumn < GameState.Instance.GridMap.ColumnNumber & ShootingRow < GameState.Instance.GridMap.RowNumber)
        {
            ProjectileGridEntity projectile =Projectiles.Create(Projectiles.Missile, Column, Row, shootingDirection);
            projectile.Position = shootingPosition;
            projectile.Move(shootingDirection, false);
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
            }
            if (Timers.Instance.OneSecondTurn)
            {
                if (shootTurn >0)
                {
                    shootCounter ++;
                    if (shootCounter >= shootTurn)
                    {
                        shootCounter = 0;
                        willShoot = true;
                    }
                }
                
                bool hasMoved = Move(Direction);
                if ((hasMoved == false) & (InThePast == false) &(touchedPlayer==false))
                {
                    changeDirection = true;
                    Direction = new Vector2(-Direction.X, -Direction.Y);
                    Move(Direction);
                }
                
            }
                
            
        }
        base.Update();
        
    }

    public override void Destroy()
    {
        GameState.Instance.enemyNumber --;
        base.Destroy();
    }

}