using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOHGeneration : MonoBehaviour
{
    [Tooltip("The number of 'units' on the x axis")]
    public int xGrid;
    [Tooltip("The number of 'units' on the z axis")]
    public int zGrid;
    [Tooltip("The side length of each grid unit")]
    public float GridSize;

    [Tooltip("Draw the grid with gizmos")]
    public bool drawGrid;

    private GameObject bohRoomPrefab;
    class room
    {
        public GameObject prefab;
        public int xLen;
        public int yLen;

        public void createRoom(int WorldX, int WorldZ)
        {
            Instantiate(prefab, new Vector3(WorldX, 0, WorldZ),Quaternion.identity);
        }
    }
    
    Dictionary<Vector2, int> grid;
    // Start is called before the first frame update
    void Start()
    {
        bohRoomPrefab = Resources.Load("bohRoomPrefab") as GameObject;
        grid = new Dictionary<Vector2, int>();
        Vector2 spot = pickRandomEmptySpot(2, 5);
        createAtGrid((int)spot.x, (int)spot.y,2,5);
     
    }

    private void createAtGrid(int x, int z, int sizeX , int sizeZ)
    {
        GameObject g =  Instantiate(bohRoomPrefab, new Vector3(x * GridSize, 0, z * GridSize), Quaternion.identity);
        g.transform.localScale = new Vector3(sizeX, 1, sizeZ);
    }

    private Vector2 pickRandomEmptySpot(int xLength, int zLength)
    {
        List<Vector2> possible = new List<Vector2>();
        for (int x = -xGrid; x < xGrid; x++)
        {
            for (int z = -zGrid; z < zGrid; z++)
            {
                if (!grid.ContainsKey(new Vector2(x, z)))
                {
                    possible.Add(new Vector2(x, z));
                }
            }
        }

        if (xLength == 1 && zLength == 1)
        {
            return possible[Random.Range(0, possible.Count - 1)];
        }
        for (int i = 0; i<possible.Count;i++)
        {
            Vector2 v = possible[i];
            for (int x = 0; x < xLength;x++)
            {
                if (grid.ContainsKey(new Vector2(v.x+x,v.y)))
                {
                    possible.Remove(v);
                }
            }

            for (int z = 0; z < zLength; z++)
            {
                if (grid.ContainsKey(new Vector2(v.x, v.y + z)))
                {
                    possible.Remove(v);
                }
            }
        }

        return possible[Random.Range(0, possible.Count - 1)];
    }

    private void OnDrawGizmos()
    {
        if (!drawGrid)
        {
            return;
        }
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(GridSize * xGrid * 2, 0, zGrid * GridSize * 2));
        for (int x = -xGrid; x < xGrid;x++)
        {
            Gizmos.DrawWireCube(new Vector3(x*GridSize,0,0), new Vector3(0, 0, zGrid * GridSize * 2));
        }

        for (int z = -zGrid; z < zGrid; z++)
        {
            Gizmos.DrawWireCube(new Vector3(0, 0, z * GridSize), new Vector3(xGrid*GridSize*2, 0, 0));
        }

    }

}
