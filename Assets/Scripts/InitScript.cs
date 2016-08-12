using UnityEngine;
using System.Collections;

public class InitScript : MonoBehaviour
{
    public int Size;
    public float SpeedWave;
    public GameObject Cube;
    private ICombustible[,] _field;
    private Graph _graph;

    void Start()
    {
        if (Size > 0)
        {
            _field = new ICombustible[Size, Size];
            _graph = new Graph(_field);
            _graph.EndWaveEvent += OnEndWave;
            CubeScript.CubeClickEvent += OnCubeClick;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    GameObject newGameObject = Instantiate(Cube);
                    newGameObject.transform.position = new Vector3(x, y);
                    _field[x, y] = newGameObject.GetComponent<CubeScript>();
                }
            }
        }

    }

    private void OnCubeClick(CubeScript sender)
    {
        _graph.StartWave(new Vector2DInt((int)sender.transform.position.x, (int)sender.transform.position.y));
    }

    private void OnEndWave()
    {
        StartCoroutine(_BurnQueue());
    }

    private IEnumerator _BurnQueue()
    {
        int numWave = 1;
        while (CubeScript.BurnQueue.Count > 0)
        {
            CubeScript cube = CubeScript.BurnQueue.Dequeue();

            if (cube.Serial > numWave)
            {   //Если пошла другая волна, делаем паузу
                yield return new WaitForSeconds(SpeedWave);
                numWave++;
            }

            cube.StartBurn();
        }
    }
}
