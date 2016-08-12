using UnityEngine;
using System.Collections.Generic;
//using Random = UnityEngine.Random;

public class CubeScript : MonoBehaviour, ICombustible
{
    public delegate void CubeClick(CubeScript sender);
    public static event CubeClick CubeClickEvent;

    public Material[] Materials;
    public static Queue<CubeScript> BurnQueue;
    public bool IsCombustible
    {
        get { return _isCombustible; }
    }

    public bool IsBurnt
    {
        get { return _isBurnt; }
    }

    public int Serial
    {
        get { return _serialBurn; }
    }

    private int _serialBurn;
    private bool _isBurnt;
    private bool _isCombustible;



    void Start()
    {
        if (BurnQueue == null)
            BurnQueue = new Queue<CubeScript>();

        int randomNum = Random.Range(1, 9);
        _isCombustible = randomNum % 2 == 0;
        GetComponent<Renderer>().sharedMaterial = IsCombustible ? Materials[1] : Materials[0];
    }

    void OnMouseDown()
    {
        if (CubeClickEvent != null && IsCombustible && !IsBurnt)
        {
            CubeClickEvent.Invoke(this);
        }
    }

    public void SetFire(int serialBurn)
    {
        if (!IsCombustible || IsBurnt)
            return;

        _serialBurn = serialBurn;
        _isBurnt = true;
        BurnQueue.Enqueue(this);
    }

    public void StartBurn()
    {
        GetComponent<Renderer>().sharedMaterial = Materials[2];
    }
}
