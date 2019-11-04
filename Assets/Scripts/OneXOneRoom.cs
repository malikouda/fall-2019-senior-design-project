using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneXOneRoom : MonoBehaviour
{
    public GameObject North;
    public GameObject South;
    public GameObject West;
    public GameObject East;

    [HideInInspector]
    public int xCoord;
    [HideInInspector]
    public int zCoord;

    public void deleteNorth()
    {
        Destroy(North);
    }

    public void deleteSouth()
    {
        Destroy(South);
    }

    public void deleteWest()
    {
        Destroy(West);
    }

    public void deleteEast()
    {
        Destroy(East);
    }
}
