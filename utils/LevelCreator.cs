using System.Numerics;
using System.Text.Json;

class LevelCreator
{
    private int columnNumber;
    private int rowNumber;
    private int size;
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

    public GridMap Create(int size = 60)
    {
        this.size = Math.Max(size, 40);
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
                        MovingObstacles.Create(MovingObstacles.Baril, j, i);
                        break;
                    case 22:
                        MovingObstacles.Create(MovingObstacles.Baril, j, i, new Vector2(),false);
                        break;
                    case 23:
                        MovingObstacles.Create(MovingObstacles.BarilExplosive, j, i);
                        break;
                    case 24:
                        MovingObstacles.Create(MovingObstacles.BarilExplosive, j, i, new Vector2(),false);
                        break;

                    case 31:
                        Obstacles.Create(Obstacles.Obstacle, j, i);
                        break;
                    case 32:
                        Obstacles.Create(Obstacles.Obstacle, j, i,  new Vector2(), false);
                        break;
                    case 33:
                        DestroyableObstacles.Create(DestroyableObstacles.Obstacle, j, i);
                        break;
                    case 34:
                        DestroyableObstacles.Create(DestroyableObstacles.Obstacle, j, i,  new Vector2(), false);
                        break;
                    case 35:
                        DestroyableObstacles.Create(DestroyableObstacles.ExplosiveObstacle, j, i);
                        break;
                    case 36:
                        DestroyableObstacles.Create(DestroyableObstacles.ExplosiveObstacle, j, i,  new Vector2(), false);
                        break;

                    case 41:
                        Enemies.Create(Enemies.Pieuvre, j, i, new Vector2(1,0));
                        break;
                    case 42:
                        Enemies.Create(Enemies.Pieuvre, j, i, new Vector2(1,0), false);
                        break;
                    case 43:
                        Enemies.Create(Enemies.Pieuvre, j, i, new Vector2(0,1));
                        break;
                    case 44:
                        Enemies.Create(Enemies.Pieuvre, j, i, new Vector2(0,1), false);
                        break;

                    case 51:
                        Enemies.Create(Enemies.Tortue, j, i, new Vector2(1,0));
                        break;
                    case 52:
                        Enemies.Create(Enemies.Tortue, j, i, new Vector2(1,0), false);
                        break;
                    case 53:
                        Enemies.Create(Enemies.Tortue, j, i, new Vector2(0,1));
                        break;
                    case 54:
                        Enemies.Create(Enemies.Tortue, j, i, new Vector2(0,1), false);
                        break;

                    case 61:
                        Enemies.Create(Enemies.Baleine, j, i, new Vector2(1,0));
                        break;
                    case 62:
                        Enemies.Create(Enemies.Baleine, j, i, new Vector2(1,0), false);
                        break;
                    case 63:
                        Enemies.Create(Enemies.Baleine, j, i, new Vector2(0,1));
                        break;
                    case 64:
                        Enemies.Create(Enemies.Baleine, j, i, new Vector2(0,1), false);
                        break;

                    case 71:
                        Enemies.Create(Enemies.FlamandRose, j, i, new Vector2(1,0));
                        break;
                    case 72:
                        Enemies.Create(Enemies.FlamandRose, j, i, new Vector2(1,0), false);
                        break;
                    case 73:
                        Enemies.Create(Enemies.FlamandRose, j, i, new Vector2(0,1));
                        break;
                    case 74:
                        Enemies.Create(Enemies.FlamandRose, j, i, new Vector2(0,1), false);
                        break;

                    case 81:
                        Enemies.Create(Enemies.Fregate, j, i, new Vector2(1,0));
                        break;
                    case 82:
                        Enemies.Create(Enemies.Fregate, j, i, new Vector2(1,0), false);
                        break;
                    case 83:
                        Enemies.Create(Enemies.Fregate, j, i, new Vector2(0,1));
                        break;
                    case 84:
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