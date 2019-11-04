using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nSizeRoom : MonoBehaviour
{

    [System.Serializable]
    public class Wall
    {
        public int xOffset;
        public int zOffset;
        public GameObject wallObj;
    }
    public Wall[] walls;
    public int roomId;
    public int xCoord;
    public int zCoord;
    public int xSize;
    public int zSize;
    public GameObject[] North;
    public GameObject[] South;
    public GameObject[] West;
    public GameObject[] East;


    public void makeDoorwayTo(Vector2 location)
    {
        foreach(Wall w in walls)
        {
            if (xCoord + w.xOffset == location.x && zCoord + w.zOffset == location.y)
            {
                Vector3 p = w.wallObj.transform.position;
                GameObject door = Instantiate(Resources.Load("DoorPrefab"), new Vector3(p.x,0,p.z), w.wallObj.transform.rotation)as GameObject;
                door.transform.localScale = w.wallObj.transform.parent.localScale;
                door.transform.parent = transform;
                Destroy(w.wallObj);
                return;
                
            }
        }
    }


    public void deleteNorth()
    {
        foreach (GameObject g in North)
        {
            Destroy(g);
        }
    }

    public void deleteSouth()
    {
        foreach (GameObject g in South)
        {
            Destroy(g);
        }
    }

    public void deleteWest()
    {
        foreach (GameObject g in West)
        {
            Destroy(g);
        }
    }

    public void deleteEast()
    {
        foreach (GameObject g in East)
        {
            Destroy(g);
        }
    }
}
