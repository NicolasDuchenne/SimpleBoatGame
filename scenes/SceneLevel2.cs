using Raylib_cs;
public class SceneLevel2:SceneGameplay
{
    public SceneLevel2():base()
    {
        columnNumber = 8;
        rowNumber = 8;
    }
    public override void Show()
    {
        base.Show();
        gridMap = new GridMap(columnNumber, rowNumber, size);
        GameState.Instance.SetGridMap(gridMap);
        Obstacles.Create(Obstacles.Obstacle, 4, 5);
        Obstacles.Create(Obstacles.Obstacle, 0, 5);
        MovableObstacles.Create(MovableObstacles.Baril, 1, 2);
        MovableObstacles.Create(MovableObstacles.Baril, 4, 4);
        MovableObstacles.Create(MovableObstacles.Baril, 6, 4);

        Player.Create(4,2);
        Enemies.Create(Enemies.Fregate, 0, 0);
    }
}