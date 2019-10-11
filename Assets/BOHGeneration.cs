using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOHGeneration : MonoBehaviour
{

    public float XCoord;
    public float ZCoord;

    class room
    {
        public GameObject prefab;
        public int xLen;
        public int yLen;

        public void createRoom(int x, int z)
        {
            Instantiate(prefab, new Vector3(x, 0, z),Quaternion.identity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void printgrid(List<List<int>> g)
    {
        string s = "";
        foreach (List<int> l in g)
        {
            foreach (int i in l)
            {
                s += i + ' ';
            }
            s += '\n';
        }
        Debug.Log(s);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(XCoord, 10, ZCoord));
    }

}
