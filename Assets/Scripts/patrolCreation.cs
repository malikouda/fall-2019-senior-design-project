using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
public class patrolCreation : MonoBehaviour
{

    public static patrolCreation instance;

    private NavMeshAgent agent;
    private List<Vector3> points;

    public class point
    {
        Vector3 pos;
        public point (Vector3 pos)
        {
            this.pos = pos;
        }
    }

    public class edge : IComparer<edge>
    {
        Vector3 start;
        Vector3 end;
        public float dist;

        public int compare(edge a, edge b)
        {
            if (a.dist < b.dist)
            {
                return -1;
            }
            if (a.dist == b.dist)
            {
                return 0;
            }
            return 1;
        }

        public edge (Vector3 start,Vector3 end)
        {
            this.start = start;
            this.end = end;
            this.dist = Mathf.Infinity;
        }

        public void AssignWeight()
        {
            //find the navmesh path between start and finish
            NavMeshPath path = new NavMeshPath();
            if (!NavMesh.CalculatePath(start, end, 0, path))
            {
                Debug.LogError("failed to find path");
            }
            else
            {
                //if the path is a straight line
                if (path.corners.Length == 0)
                {
                    dist = Vector3.Distance(start, end);
                    return;
                }

                //sum the first straight away
                Vector3 lastCorner = path.corners[0];
                dist += Vector3.Distance(start , lastCorner);

                //sum distances between each corner
                foreach (Vector3 v in path.corners)
                {
                    dist += Vector3.Distance(lastCorner, v);
                    lastCorner = v;
                }

                //sum the last distance
                dist += Vector3.Distance(lastCorner, end);
            }
        }

        public int Compare(edge x, edge y)
        {
            throw new System.NotImplementedException();
        }

        public void showEdge ()
        {
            Debug.DrawLine(start, end,Color.cyan,100);
        }
    }

    private void Awake()
    {
        //create a singleton
        if (instance == null)
        {
            instance = this;
        }
        agent = GetComponent<NavMeshAgent>();
    }

    //called from Gamemanager after Maze Creation
    public static void createPatrols()
    {
        instance.makePatrols();
    }

    //creates a minimum spanning tree
    public void makePatrols()
    {
        
        //gets a random point from each generated maze room
        points = new List<Vector3>();
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("room");
        foreach (GameObject g in rooms)
        {
            List<Vector3> cells = new List<Vector3>();
            foreach (Transform child in g.GetComponentsInChildren<Transform>())
            {
                Debug.Log(child);
                if (child.gameObject.tag == "cell")
                {
                    cells.Add(child.gameObject.transform.position);
                }
            }

            if (cells.Count != 0)
            {
                points.Add(cells[Random.Range(0, cells.Count)]);

            }
        }



        //creates a set of all edges on the map
        List<edge> map = new List<edge>();
        foreach(Vector3 a in points)
        {
            foreach(Vector3 b in points)
            {
                map.Add(new edge(a, b));
            }
        }

        //finds the weight on all edges (will take a long time)
        foreach(edge e in map)
        {
            e.showEdge();
            e.AssignWeight();
        }


        



    }
}
