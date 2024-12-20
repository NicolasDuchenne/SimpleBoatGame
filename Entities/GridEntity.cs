using Raylib_cs;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

public class GridEntity: Entity
{
    public int Column {get; protected set;}
    public int Row {get; protected set;}
    public bool CanMove {get; protected set;} = false;
    public bool CanMoveEntities {get; protected set;} = true;
    
    public GridEntity(Texture2D texture, int column, int row): base(texture, GetCenterPositionFromTile(column, row))
    {
        (column, row) = ClampPosition(column, row);
        Column = column;
        Row = row; 
        GameState.Instance.GridMap.Tiles[column][row].setEntity(this);
    }

    public bool Move(Vector2 direction)
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
        if (GameState.Instance.GridMap.Tiles[Column][Row].Entity is not null)
        {
            GridEntity collidedEntity = GameState.Instance.GridMap.Tiles[Column][Row].Entity;
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

    public override void Update()
    {
        base.Update();
        Position = GetCenterPositionFromTile(Column, Row);
    }

    private static Vector2 GetCenterPositionFromTile(int column, int row)
    {
        (column, row) = ClampPosition(column, row);
        return GameState.Instance.GridMap.Tiles[column][row].CenterPosition;
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