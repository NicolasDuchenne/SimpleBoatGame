using System.Numerics;
using Raylib_cs;

public class GridMap
{
    public int ColumnNumber {get; private set;}
    public int RowNumber {get; private set;}
    public int Size {get ; private set;}
    public Color Color {get; private set;} = Color.Black;

    private int rangeSendInPast;

    public Dictionary<int, Dictionary<int, Tile>> Tiles {get; private set;}

    private float columnOffset;
    private float rowOffset;

    public GridMap(int columnNumber, int rowNumber, int size, int rangeSendInPast=1)
    {
        this.rangeSendInPast = rangeSendInPast;
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
    private void ResetSendToPast()
    {
        for (int i = 0; i<ColumnNumber; i++)
        {
            for (int j=0; j<RowNumber; j++)
            {
                Tiles[i][j].CanBeSentInThepast = false;
            }
        }
    }
    private void SetSendToPast()
    {
        for (int i = 0; i<ColumnNumber; i++)
        {
            for (int j=0; j<RowNumber; j++)
            {
                if (Tiles[i][j].GridEntity is not null)
                {
                    if (Tiles[i][j].GridEntity.name.Contains("player"))
                    {
                        SetSendToPastOnNeighbors(i, j);
                    }
                }
            }
        }
        
    }
    private void SetSendToPastOnNeighbors(int i, int j)
    {
        for (int col=-rangeSendInPast; col<=rangeSendInPast; col++)
        {
            for (int row = -rangeSendInPast; row<=rangeSendInPast; row++)
            {
                if(0<=i+col & i+col< GameState.Instance.GridMap.ColumnNumber & 0<=j+row&j + row < GameState.Instance.GridMap.RowNumber)
                {
                    Tiles[i+col][j+row].CanBeSentInThepast = true;
                }
            }
        
        }
    }

    public void Update()
    {
        ResetSendToPast();
        SetSendToPast();
        for (int i = 0; i<ColumnNumber; i++)
        {
            for (int j=0; j<RowNumber; j++)
            {
                Tiles[i][j].Update();
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
    private Color CanBeSendInpastColor = new Color(255,255,255, 10);

    private Rectangle rect ;

    private float turnInPast = 0;
    
    private int maxTurnInPast;
    private bool processPast = false;
    private float pastTimer=0;
    public bool CanBeSentInThepast=false;


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
                {
                    GridEntity.Hit();
                } 
                if (gridEntity.CanBeHurt)
                {
                    gridEntity.Hit();
                }
                if ((GridEntity.Destroyed) & (gridEntity.Destroyed))
                {
                    removeEntity(GridEntity.name);
                    removeEntity(gridEntity.name);
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
        bool mouseIsInRect = Raylib.CheckCollisionPointRec(mousePos, rect);
        if ((PastGridEntity is null)&(GridEntity is not null) &(mouseIsInRect) & (GameState.Instance.elemInPast < GameState.Instance.MaxElemInPast))
        {
            if (CanBeSentInThepast&(GridEntity.CanBeSentInThepast) & (GridEntity.Moving==false))
            {
                isMousedOver = true;
                GridEntity.Sprite.ActivateShader();
            }
                
        }
        if ((GridEntity is not null) &(mouseIsInRect==false))
        {
            GridEntity.Sprite.DeactivateShader();
        }
        if ((isMousedOver) & (Raylib.IsMouseButtonPressed(MouseButton.Left)) )
        {
            maxTurnInPast = GameState.Instance.MaxTurnInPast;
            PastGridEntity = GridEntity;
            GridEntity.InThePast = true;
            GridEntity.Sprite.ActivateShader();
            removeEntity(GridEntity.name);
            GameState.Instance.elemInPast ++;
            processPast = true;

            Score.Instance.addSendToPast();
            
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
                    PastGridEntity.Sprite.DeactivateShader();
                    setEntity(PastGridEntity);
                    PastGridEntity = null;
                    turnInPast = 0;
                    GameState.Instance.elemInPast = 0;
                    processPast = false;
                }
            }
            pastTimer+=Raylib.GetFrameTime();
            if (pastTimer>=1)
            //if (PastGridEntity is not null&Timers.Instance.OneSecondTurn)
            {      
                pastTimer = 0;
                turnInPast = turnInPast + 1f;
            }
        }
        
    }



    public void Update()
    {
        SendToPast();
        processPastEntities();
    }

    public void removeEntity(string name)
    {
        if (GridEntity is not null)
        {
            // To make sure that we do not remove an object we did not expect to remove
            if (GridEntity.name == name)
            {
                GridEntity = null;
            }
        }
    }

    public void Draw()
    {
        if(CanBeSentInThepast)
        {
            Raylib.DrawRectangleRec(new Rectangle(Position, Size, Size), CanBeSendInpastColor);
        }
        if (isMousedOver)
        {
            //Raylib.DrawRectangleRec(new Rectangle(Position, Size, Size), MouseOverColor);
        }
        if (PastGridEntity is not null)
        {
            //Raylib.DrawRectangleRec(new Rectangle(Position, Size, Size), MouseOverColor);
            Raylib.DrawText($"{Math.Ceiling(maxTurnInPast-turnInPast).ToString()}", (int)(Position.X+3/4f*Size), (int)(Position.Y+Size/12f), 12, Color);
        }
        //Raylib.DrawRectangleLinesEx(new Rectangle(Position, Size, Size), 1, Color);
    }
}