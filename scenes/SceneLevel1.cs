using System.Numerics;
using Raylib_cs;
public class SceneLevel1:SceneGameplay
{
    public override void Show()
    {
        base.Show();
        columnNumber = 7;
        rowNumber = 7;
        gridMap = new GridMap(columnNumber, rowNumber, size);
        GameState.Instance.SetGridMap(gridMap);
        Obstacles.Create(Obstacles.Obstacle, 4, 5);
        Obstacles.Create(Obstacles.Obstacle, 4, 0, false);
        MovableObstacles.Create(MovableObstacles.Baril, 2, 1);
        MovableObstacles.Create(MovableObstacles.Baril, 4, 4, false);
        MovableObstacles.Create(MovableObstacles.Baril, 6, 4);

        Player.Create(2,3);
        Enemies.Create(Enemies.Fregate, 0, 0, new Vector2(1,0), false);
    }
}