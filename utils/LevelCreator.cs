using System.Numerics;
using System.Text.Json;

class LevelCreator
{
    protected int columnNumber;
    protected int rowNumber;
    protected int size = 40;
    public Dictionary<int, Dictionary<int, int>> Matrix {get; private set;}
    public LevelCreator (String json)
    {
        Matrix = DeserializeMatrix(json);
        
    }
    private Dictionary<int, Dictionary<int, int>> DeserializeMatrix(String json)
    {
        // Deserialize JSON into a List of Lists
        var matrixList = JsonSerializer.Deserialize<List<List<int>>>(json);

        // Convert to Dictionary<int, Dictionary<int, int>>
        var matrixDict = new Dictionary<int, Dictionary<int, int>>();
        for (int i = 0; i < matrixList.Count; i++)
        {
            var rowDict = new Dictionary<int, int>();
            for (int j = 0; j < matrixList[i].Count; j++)
            {
                rowDict[j] = matrixList[i][j];
            }
            matrixDict[i] = rowDict;
        }
        return matrixDict;
    }

    public GridMap Create()
    {
        rowNumber = Matrix.Count();
        columnNumber = Matrix[0].Count();
        GridMap gridMap = new GridMap(columnNumber, rowNumber, size);
        GameState.Instance.SetGridMap(gridMap);
        for (int i = 0; i < Matrix.Count; i++)
        {
            for (int j = 0; j < Matrix[i].Count; j++)
            {
                switch (Matrix[i][j])
                {
                    case 0:
                        break;
                    case 1:
                        Player.Create(j,i);
                        break;
                    case 21:
                        MovableObstacles.Create(MovableObstacles.Baril, j, i);
                        break;
                    case 22:
                        MovableObstacles.Create(MovableObstacles.Baril, j, i, false);
                        break;
                    case 31:
                        Obstacles.Create(Obstacles.Obstacle, j, i);
                        break;
                    case 32:
                        Obstacles.Create(Obstacles.Obstacle, j, i, false);
                        break;
                    case 41:
                        Enemies.Create(Enemies.Fregate, j, i, new Vector2(1,0));
                        break;
                    case 42:
                        Enemies.Create(Enemies.Fregate, j, i, new Vector2(1,0), false);
                        break;
                    case 43:
                        Enemies.Create(Enemies.Fregate, j, i, new Vector2(0,1));
                        break;
                    case 44:
                        Enemies.Create(Enemies.Fregate, j, i, new Vector2(0,1), false);
                        break;
                    
                    default:
                        break;
                }
        
                    
            }
        }
        return gridMap;
    }
}