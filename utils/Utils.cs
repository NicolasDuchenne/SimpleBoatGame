using System.Numerics;

public static class Utils
{
    public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
    {
        return new Vector2(
            a.X + (b.X - a.X) * t,
            a.Y + (b.Y - a.Y) * t
        );
    }
}