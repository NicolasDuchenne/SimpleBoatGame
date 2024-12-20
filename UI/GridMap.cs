using System.Numerics;
using System.Security.Cryptography;
using Raylib_cs;

public class GridMap
{
    public int ColumnNumber {get; private set;}
    public int RowNumber {get; private set;}
    public int Size {get ; private set;}
    public Color Color {get; private set;} = Color.White;

    public Dictionary<int, Dictionary<int, Tile>> Tiles {get; private set;}

    private float columnOffset;
    private float rowOffset;

    public GridMap(int columnNumber, int rowNumber, int size)
    {
        ColumnNumber = columnNumber;
        RowNumber = rowNumber;
        Size = size;
        columnOffset = (GameState.Instance.GameScreenWidth/(float)Size-ColumnNumber)/2;
        rowOffset = (GameState.Instance.GameScreenHeight/(float)Size-RowNumber)/2;
        Tiles = new Dictionary<int, Dictionary<int, Tile>>();
        for (int i = 0; i<ColumnNumber; i++)
        {
            Tiles[i] = new Dictionary<int, Tile>();
            for (int j=0; j<RowNumber; j++)
            {
                Tiles[i][j] = new Tile(new Vector2((i+columnOffset)*Size, (j+rowOffset)*Size), Size, Color);
            }
        }
    }

    public void Draw()
    {
        foreach(var item in Tiles)
        {
            foreach(var tile in item.Value)
            {
                tile.Value.Draw();
            }
        }
   
    }
}

public class Tile
{
    public Vector2 Position {get; private set;}
    public int Size {get; private set;}
    public Vector2 CenterPosition {get; private set;}

    public Color Color {get; private set;}
    public GridEntity? Entity {get; private set;}

    public Tile(Vector2 position, int size, Color color)
    {
        Position = position;
        Size = size;
        CenterPosition = Position + new Vector2(size/2, size/2);
        Color = color;
    }

    public void setEntity(GridEntity entity)
    {
        if (Entity is not null)
            throw new Exception($"tile {Position} should be emptied before being set");
        Entity = entity; 
    }

    public void removeEntity()
    {
        Entity = null;
    }

    public void Draw()
    {
        Raylib.DrawRectangleLinesEx(new Rectangle(Position, Size, Size), 1, Color);
    }
}