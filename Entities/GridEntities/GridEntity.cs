using Raylib_cs;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

public class GridEntity: Entity
{
    static int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
    public int Column {get; protected set;}
    public int Row {get; protected set;}
    public bool CanMove {get; protected set;} = false;
    public bool CanMoveEntities {get; protected set;} = false;

    public bool CanBeHurt {get; protected set;} = false;

    public bool InThePast = false;
    
    public GridEntity(Sprite sprite, int column, int row): base(sprite, GetCenterPositionFromTile(column, row))
    {
        (column, row) = ClampPosition(column, row);
        Column = column;
        Row = row; 
        GameState.Instance.GridMap.Tiles[column][row].setEntity(this);
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
            if ((Column==baseColumn)&(Row==baseRow))
            {
                return false;
            }
            if (GameState.Instance.GridMap.Tiles[Column][Row].GridEntity is not null)
            {
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
            return true;
        }
        return false;
        
    }

    public override void Update()
    {
        if (InThePast == false)
        {
            base.Update();
            Position = GetCenterPositionFromTile(Column, Row);
        }
    }

    public override void Draw()
    {
        if (InThePast == false)
        {
            base.Draw();
        }
    }

    private static Vector2 GetCenterPositionFromTile(int column, int row)
    {
        (column, row) = ClampPosition(column, row);
        return GameState.Instance.GridMap.Tiles[column][row].CenterPosition;
    }

    public virtual void Hit()
    {
        if (InThePast == false)
        {
            Console.WriteLine("I'm Hit");
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