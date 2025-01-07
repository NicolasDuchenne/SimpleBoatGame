using System.Numerics;
using System.Security.Cryptography;
using Raylib_cs;

public class GridMap
{
    public int ColumnNumber {get; private set;}
    public int RowNumber {get; private set;}
    public int Size {get ; private set;}
    public Color Color {get; private set;} = Color.Black;

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

    public void Update()
    {
        foreach(var item in Tiles)
        {
            foreach(var tile in item.Value)
            {
                tile.Value.Update();
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
        Raylib.DrawRectangleLinesEx(new Rectangle(Tiles[0][0].Position, ColumnNumber*Size, RowNumber*Size), 1, Color);
   
    }

    public Vector2 PathFind(int TargetColumn, int TargetRow)
    {
        return new Vector2(1,0);
    }

}

public class Tile
{
    public Vector2 Position {get; private set;}
    public int Size {get; private set;}
    public Vector2 CenterPosition {get; private set;}

    public Color Color {get; private set;}
    public GridEntity? GridEntity {get; private set;}
    public GridEntity PastGridEntity {get; private set;}

    private bool isMousedOver = false;

    private Color MouseOverColor = new Color(128, 128, 128, 128);

    private Rectangle rect ;

    private float turnInPast = 0;
    
    private int maxTurnInPast;
    private bool processPast = false;

    public Tile(Vector2 position, int size, Color color)
    {
        Position = position;
        Size = size;
        CenterPosition = Position + new Vector2(size/2, size/2);
        Color = color;
        rect = new Rectangle(Position.X, Position.Y, size, size);
    }

    public void setEntity(GridEntity gridEntity)
    {
        if (GridEntity is not null) 
        {
            if ((GridEntity.CanBeHurt==false) & (gridEntity.CanBeHurt==false))
            {
                throw new Exception($"two entities on tile {Position} can't be hurt are are trying to be set");
            }
            else
            {
                if (GridEntity.CanBeHurt)
                    GridEntity.Hit();
                if (gridEntity.CanBeHurt)
                    gridEntity.Hit();
                if ((GridEntity.Destroyed) & (gridEntity.Destroyed))
                {
                    removeEntity();
                }
            }
        } 
        if (gridEntity.Destroyed==false)
        {
            GridEntity = gridEntity; 
        }
            
    }
    public void SendToPast()
    {
        isMousedOver = false;
        Vector2 mousePos = GameState.Instance.Mouse.MousePos;
        if ((GridEntity is not null) &(Raylib.CheckCollisionPointRec(mousePos, rect)) )
        {
            if ((GridEntity.CanBeSentInThepast) & (GridEntity.Moving==false))
                isMousedOver = true;
        }
        if ((isMousedOver) & (Raylib.IsMouseButtonPressed(MouseButton.Left)) & (GameState.Instance.elemInPast < GameState.Instance.MaxElemInPast))
        {
            maxTurnInPast = GameState.Instance.MaxTurnInPast;
            PastGridEntity = GridEntity;
            GridEntity.InThePast = true;
            removeEntity();
            GameState.Instance.elemInPast ++;
            processPast = true;
        }
    }

    public void processPastEntities()
    {
        if (processPast)
        {
            // We test this before incrementing so that the set entity is made after either Player or all enemy have played there turn
            if (turnInPast >= maxTurnInPast)
            {
                bool resetPastEntity = false;
                if (GridEntity is null) 
                    resetPastEntity = true;
                // wait for current entity to arrive before resetting past for a better visual
                else if ((GridEntity is not null) & (GridEntity.Moving==false))
                    resetPastEntity = true;
                if (resetPastEntity) 
                {
                    PastGridEntity.InThePast = false;
                    setEntity(PastGridEntity);
                    PastGridEntity = null;
                    turnInPast = 0;
                    GameState.Instance.elemInPast = 0;
                    processPast = false;
                }
            }
            //if ((PastGridEntity is not null)&((Timers.Instance.PlayerPlayTurn)|(Timers.Instance.EnemyPlayTurn)))
            if (PastGridEntity is not null&Timers.Instance.OneSecondTurn)
            {      
                turnInPast ++;
            }
        }
        
    }

    public void Update()
    {
        SendToPast();
        processPastEntities();
        
    }

    public void removeEntity()
    {
        GridEntity = null;
    }

    public void Draw()
    {
        if (isMousedOver)
        {
            Raylib.DrawRectangleRec(new Rectangle(Position, Size, Size), MouseOverColor);
        }
        if (PastGridEntity is not null)
        {
            Raylib.DrawRectangleRec(new Rectangle(Position, Size, Size), MouseOverColor);
            Raylib.DrawText($"{Math.Ceiling(maxTurnInPast-turnInPast).ToString()}", (int)Position.X+Size/2, (int)Position.Y+Size/2, 12, Color);
        }
        //Raylib.DrawRectangleLinesEx(new Rectangle(Position, Size, Size), 1, Color);
    }
}