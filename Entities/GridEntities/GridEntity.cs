using Raylib_cs;
using System.Numerics;

public class GridEntity: Entity
{
    static List<Vector2> directions = new List<Vector2>
    {
        new Vector2(0, 1),  // Up
        new Vector2(1, 0),  // Right
        new Vector2(0, -1), // Down
        new Vector2(-1, 0)  // Left
    };
    public int Column {get; protected set;}
    public int Row {get; protected set;}
    private int LastTriedColumn;
    private int LastTriedRow;
    public bool CanBeMoved {get; protected set;} = false;
    public bool CanMoveEntities {get; protected set;} = false;

    public bool CanBeHurt {get; protected set;} = true;

    public bool CanHurt {get; protected set;} = false;
    public bool CanHurtPlayer {get; protected set;} = false;
    public bool IsPlayer {get; protected set;} = false;

    public bool InThePast = false;

    public bool CanBeSentInThepast  {get; protected set;}

    public bool Moving=false;

    public float speed = 127;

    public Vector2 TargetPosition = new Vector2();
    public new Vector2 Direction {get; private set;}
    public bool positionWasClamped = false;
    public bool touchedPlayer = false;
    
    public GridEntity(Sprite sprite, int column, int row, Vector2 direction=new Vector2(), bool canBeSentInThePast = true): base(sprite, GetCenterPositionFromTile(column, row))
    {
        if (direction==new Vector2())
            direction = new Vector2(1, 0);
        UpdateDirection((Vector2)direction);
        (column, row) = ClampPosition(column, row);
        Column = column;
        Row = row; 
        Position = GetCenterPositionFromTile(Column, Row);
        TargetPosition = Position;
        GameState.Instance.GridMap.Tiles[column][row].setEntity(this);
        CanBeSentInThepast = canBeSentInThePast;

    }

    public void UpdateDirection(Vector2 inputDirection)
    {
        if (directions.Contains(inputDirection))
        {
            Rotation = GetAngleFromDirection(inputDirection);
            Direction = inputDirection;
        }
        else
        {
            throw new Exception($"Direction {inputDirection} not in {directions}!");
        }
    }
    protected virtual float GetAngleFromDirection(Vector2 direction)
    {
        
        if (direction == new Vector2(1, 0)) 
        {
            Flip = false;
        }
        if (direction == new Vector2(-1, 0))
        {
            Flip = true;
        } 
        return 0f; // Default
    }
    

    public bool Move(Vector2 direction)
    {
        if (InThePast == false)
        {
            touchedPlayer = false;
            positionWasClamped = false;
            UpdateDirection(direction);
            int baseColumn = Column;
            int baseRow = Row;
            Column += (int)Direction.X;
            Row += (int)Direction.Y;
            (Column, Row) = ClampPosition(Column, Row);
            (LastTriedColumn, LastTriedRow) = (Column, Row);
            if ((Column==baseColumn)&(Row==baseRow))
            {
                positionWasClamped = true;
                return false;
            }
            if (GameState.Instance.GridMap.Tiles[Column][Row].GridEntity is not null)
            {
                Moving = true;
                TargetPosition = GetBordureSpritePositionFromTile(Column, Row, Direction);
                GridEntity collidedEntity = GameState.Instance.GridMap.Tiles[Column][Row].GridEntity;
                bool hasMoved = false;
                if (collidedEntity.IsPlayer)
                    touchedPlayer = true;
                if ((collidedEntity.CanBeMoved) & (CanMoveEntities))
                {
                    hasMoved = collidedEntity.Move(Direction);
                }
                if (hasMoved == false)
                {
                    Column = baseColumn;
                    Row = baseRow;
                    return false;
                }   
            }
            GameState.Instance.GridMap.Tiles[baseColumn][baseRow].removeEntity();
            GameState.Instance.GridMap.Tiles[Column][Row].setEntity(this);
            TargetPosition = GetCenterPositionFromTile(Column, Row);
            Moving = true;
            return true;
        }
        return false;
        
    }

    public override void Destroy()
    {
        if (Destroyed == false)
        {
            base.Destroy();
            Destructions.Create(Position);
        } 
    }

    private void Hurt()
    {
        if (CanBeHurt)
            Hit();
        if (GameState.Instance.GridMap.Tiles[LastTriedColumn][LastTriedRow].GridEntity.CanBeHurt)
            GameState.Instance.GridMap.Tiles[LastTriedColumn][LastTriedRow].GridEntity.Hit();
        if (GameState.Instance.GridMap.Tiles[LastTriedColumn][LastTriedRow].GridEntity.Destroyed) 
        {
            GameState.Instance.GridMap.Tiles[LastTriedColumn][LastTriedRow].removeEntity();
        }
        if (Destroyed)
        {
            GameState.Instance.GridMap.Tiles[Column][Row].removeEntity();
        }
    }

    private void ProcessMovement()
    {
        
        if (Moving)
        {
            float dist = (TargetPosition-Position).Length();
            if (dist < 1)
            {
                Moving = false;
                Position = TargetPosition;
            }
            else
            {
                Position = Position + Vector2.Normalize(TargetPosition-Position) * speed *Raylib.GetFrameTime();
            }
        }
        else
        {
            //If we tried to move a inmovable object, we go to the object position
            Vector2 expectedPosition = GetCenterPositionFromTile(Column, Row);
            if (TargetPosition!=expectedPosition)
            {
                // If the object left, we go to the last tried pos
                if (GameState.Instance.GridMap.Tiles[LastTriedColumn][LastTriedRow].GridEntity is null)
                {
                    Vector2 direction = Vector2.Normalize(GetCenterPositionFromTile(LastTriedColumn, LastTriedRow) - GetCenterPositionFromTile(Column, Row));
                    Move(direction);
                }
                // If the object is still here, we go either kill it, or go back to the last tried position
                else
                {
                    // Hurt if a canHurt object encouters any object
                    if (CanHurt || GameState.Instance.GridMap.Tiles[LastTriedColumn][LastTriedRow].GridEntity.CanHurt)
                    {
                        Hurt();
                    }
                    // Hurt if an enemie encouter the player
                    else if ((GameState.Instance.GridMap.Tiles[LastTriedColumn][LastTriedRow].GridEntity.IsPlayer&CanHurtPlayer)||(IsPlayer&GameState.Instance.GridMap.Tiles[LastTriedColumn][LastTriedRow].GridEntity.CanHurtPlayer))
                    {
                        Hurt();
                    }
                    else
                    {
                        Moving = true;
                        TargetPosition = expectedPosition;
                    }
                    
                }
                
                
            }
        }
    }

    public override void Update()
    {
        if (InThePast == false)
        {
            base.Update();
            ProcessMovement();
        }
    }

    public override void Draw(Color? color = null)
    {
        Color? drawColor = color;
        if (InThePast)
        {
            drawColor = new Color(255, 255, 255, 60);
        }
        else if (CanBeSentInThepast == false)
        {
            drawColor = Color.SkyBlue;
        }

        base.Draw(drawColor);
    }

    private static Vector2 GetCenterPositionFromTile(int column, int row)
    {
        (column, row) = ClampPosition(column, row);
        return GameState.Instance.GridMap.Tiles[column][row].CenterPosition;
    }

    private static Vector2 GetBordureSpritePositionFromTile(int column, int row, Vector2 direction)
    {
        (column, row) = ClampPosition(column, row);
        Vector2 centerPos = GameState.Instance.GridMap.Tiles[column][row].CenterPosition;
        return centerPos-direction*GameState.Instance.GridMap.Tiles[column][row].GridEntity.Sprite.Width;
    }

    public virtual void Hit()
    {
        if (InThePast == false)
        {
            Destroy();
        }
    }

    protected static (int, int) ClampPosition(int column, int row)
    {
        if (column < 0)
            column = 0;
        else if  (column > GameState.Instance.GridMap.ColumnNumber-1)
            column = GameState.Instance.GridMap.ColumnNumber-1;
        if (row < 0)
            row = 0;
        else if  (row > GameState.Instance.GridMap.RowNumber-1)
            row = GameState.Instance.GridMap.RowNumber-1;
        return (column, row);
    }
}