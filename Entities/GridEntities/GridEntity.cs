using Raylib_cs;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Numerics;

public class GridEntity: Entity
{
    static int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
    public int Column {get; protected set;}
    public int Row {get; protected set;}
    private int LastTriedColumn;
    private int LastTriedRow;
    public bool CanMove {get; protected set;} = false;
    public bool CanMoveEntities {get; protected set;} = false;

    public bool CanBeHurt {get; protected set;} = false;

    public bool InThePast = false;

    public bool CanBeSentInThepast  {get; protected set;}

    public bool Moving=false;

    public float speed = 5;

    public Vector2 TargetPosition = new Vector2();


    
    public GridEntity(Sprite sprite, int column, int row, bool canBeSentInThePast = true): base(sprite, GetCenterPositionFromTile(column, row))
    {
        (column, row) = ClampPosition(column, row);
        Column = column;
        Row = row; 
        Position = GetCenterPositionFromTile(Column, Row);
        TargetPosition = Position;
        GameState.Instance.GridMap.Tiles[column][row].setEntity(this);
        CanBeSentInThepast = canBeSentInThePast;

    }

    public bool Move(Vector2 direction)
    {
        if (InThePast == false)
        {
            int baseColumn = Column;
            int baseRow = Row;
            Column += (int)direction.X;
            Row += (int)direction.Y;
            (Column, Row) = ClampPosition(Column, Row);
            (LastTriedColumn, LastTriedRow) = (Column, Row);
            if ((Column==baseColumn)&(Row==baseRow))
            {
                return false;
            }
            if (GameState.Instance.GridMap.Tiles[Column][Row].GridEntity is not null)
            {
                Moving = true;
                TargetPosition = GetBordureSpritePositionFromTile(Column, Row, direction);
                GridEntity collidedEntity = GameState.Instance.GridMap.Tiles[Column][Row].GridEntity;
                bool hasMoved = false;
                if ((collidedEntity.CanMove) & (CanMoveEntities))
                {
                    hasMoved = collidedEntity.Move(direction);
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
                Position = Position + Vector2.Normalize(TargetPosition-Position) * 150 *Raylib.GetFrameTime();
            }
        }
        else
        {
            //If we tried to move a inmovable object, we go back to start up position
            Vector2 expectedPosition = GetCenterPositionFromTile(Column, Row);
            if (TargetPosition!=expectedPosition)
            {
                if (GameState.Instance.GridMap.Tiles[LastTriedColumn][LastTriedRow].GridEntity is null)
                {
                    Vector2 direction = Vector2.Normalize(GetCenterPositionFromTile(LastTriedColumn, LastTriedRow) - GetCenterPositionFromTile(Column, Row));
                    Move(direction);
                }
                else
                {
                    Moving = true;
                    TargetPosition = expectedPosition;
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