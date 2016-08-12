using System.Collections.Generic;

public class Graph
{
    public delegate void EndWave();
    public event EndWave EndWaveEvent;

    private ICombustible[,] _field;
    private int _sideCount;

    private readonly Vector2DInt[] _offsets =
    {
        new Vector2DInt(0, 1),
        new Vector2DInt(1, 1),
        new Vector2DInt(1, 0),
        new Vector2DInt(1, -1),
        new Vector2DInt(0, -1),
        new Vector2DInt(-1, -1),
        new Vector2DInt(-1, 0),
        new Vector2DInt(-1, 1)
    };

    public Graph(ICombustible[,] field)
    {
        _sideCount = (int)field.GetLongLength(0);
        _field = field;
    }

    public void StartWave(Vector2DInt startPoint)
    {
        Queue<Vector2DInt> queue = new Queue<Vector2DInt>();
        queue.Enqueue(new Vector2DInt(startPoint.x, startPoint.y));
        int numWave = 1;

        _field[startPoint.x, startPoint.y].SetFire(numWave);

        while (queue.Count > 0)
        {
            numWave++;
            for (int i = queue.Count; i > 0; i--)
            {
                _OneWave(queue, queue.Dequeue(), numWave);
            }
        }

        if (EndWaveEvent != null)
            EndWaveEvent.Invoke();
    }

    private void _OneWave(Queue<Vector2DInt> queue, Vector2DInt point, int numWave)
    {
        foreach (Vector2DInt offset in _offsets)
        {
            Vector2DInt newPoint = point + offset;

            if (_IsValid(newPoint) 
                && _field[newPoint.x, newPoint.y].IsCombustible
                && !_field[newPoint.x, newPoint.y].IsBurnt)
            {
                _field[newPoint.x, newPoint.y].SetFire(numWave);
                queue.Enqueue(new Vector2DInt(newPoint.x, newPoint.y));
            }
        }
    }

    private bool _IsValid(Vector2DInt point)
    {
        return (point.x > -1 && point.y > -1 && point.x < _sideCount && point.y < _sideCount);
    }
}
