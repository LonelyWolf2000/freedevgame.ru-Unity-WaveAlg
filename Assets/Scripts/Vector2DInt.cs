public class Vector2DInt
{
    public int x;
    public int y;

    public Vector2DInt(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Vector2DInt operator +(Vector2DInt a, Vector2DInt b)
    {
        return new Vector2DInt(a.x + b.x, a.y + b.y);
    }

    public static Vector2DInt operator -(Vector2DInt a, Vector2DInt b)
    {
        return new Vector2DInt(a.x - b.x, a.y - b.y);
    }
}

