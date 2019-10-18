using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
public class patrolCreation : MonoBehaviour
{

    public static patrolCreation instance;
    [Tooltip("The number of guards to be instantiated")]
    public int NumGuards;
    private NavMeshAgent agent;
    private List<point> points;

    
    public class point
    {
        public Vector3 pos;
        public List<edge> edges;
        public point (Vector3 pos)
        {
            edges = new List<edge>();
            this.pos = pos;
        }

        public void assignEdge (edge e)
        {
            this.edges.Add(e);
        }

        public void removeEdge (edge e)
        {
            this.edges.Remove(e);
        }

        public List<point> getAdj()
        {
            List<point> output = new List<point>();
            foreach(edge e in edges)
            {
                if (e.start != this)
                {
                    output.Add(e.start);
                }
                if (e.end != this)
                {
                    output.Add(e.end);
                }
            }

            return output;
        }
    }

    public class edge : IComparer<edge>
    {

        public point start;
        public point end;
        public float dist;
        private NavMeshPath path = new NavMeshPath();

        public edge (point start,point end)
        {
            this.start = start;
            this.end = end;
            this.dist = Mathf.Infinity;
        }

        public void AssignWeight()
        {
            //find the navmesh path between start and finish
            dist = 0;
            path = new NavMeshPath();
            if (!NavMesh.CalculatePath(start.pos, end.pos, NavMesh.AllAreas, path))
            {
                Debug.LogError("failed to find path");
            }
            else
            {
                //if the path is a straight line
                if (path.corners.Length == 0)
                {
                    dist = Vector3.Distance(start.pos, end.pos);
                    return;
                }

                //sum the first straight away
                Vector3 lastCorner = path.corners[0];
                dist += Vector3.Distance(start.pos , lastCorner);

                //sum distances between each corner
                foreach (Vector3 v in path.corners)
                {
                    dist += Vector3.Distance(lastCorner, v);
                    lastCorner = v;
                    
                }

                //sum the last distance
                dist += Vector3.Distance(lastCorner, end.pos);


            }
        }

        public void showEdge ()
        {
            Debug.Log(dist);
            Vector3 lastEdge = path.corners[0];
            foreach(Vector3 c in path.corners)
            {
                Debug.DrawLine(lastEdge, c, Color.green,100);
                lastEdge = c;
            }
        }

        public void addtoTree()
        {
            start.assignEdge(this);
            end.assignEdge(this);
        }

        public void removeFromTree()
        {
            start.removeEdge(this);
            end.removeEdge(this);
        }

        public List<edge> listEdges()
        {
            List<edge> output = new List<edge>();
            foreach (edge e in end.edges)
            {
                output.Add(e);
            }
            foreach (edge e in start.edges)
            {
                output.Add(e);
            }

            return output;
        }
        int IComparer<edge>.Compare(edge a, edge b)
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


    private bool wouldMakeCyclical(edge e)
    {
        e.addtoTree();
        List<point> v = new List<point>();
        v.Add(e.start);
        Stack<point> nextPoints = new Stack<point>();
        foreach (point p in e.start.getAdj())
        {
            nextPoints.Push(p);
        }
        if (isCyclical(nextPoints, v, e.start))
        {
            e.removeFromTree();
            return true;
        }else
        {
            return false;
        }
    }

    private bool isCyclical(Stack<point> current,List<point> visited, point parent)
    {
        if (current.Count == 0)
        {
            return false;
        }
        point nextPoint = current.Pop();
        visited.Add(nextPoint);

        foreach (point p in nextPoint.getAdj())
        {
            if (visited.Contains(p))
            {
                if (p != parent)
                {
                    return true;
                }

            }else
            {
                current.Push(p);
                if (isCyclical(current, visited, p))
                {
                    return true;
                }
            }

        }

        return false;
    }
    //creates a minimum spanning tree
    public void makePatrols()
    {
        
        //gets a random point from each generated maze room
        points = new List<point>();
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("room");
        foreach (GameObject g in rooms)
        {
            List<Vector3> cells = new List<Vector3>();
            foreach (Transform child in g.GetComponentsInChildren<Transform>())
            {
                if (child.gameObject.tag == "cell")
                {
                    cells.Add(child.gameObject.transform.position);
                }
            }

            if (cells.Count != 0)
            {
                points.Add(new point(cells[Random.Range(0, cells.Count)]));

            }
        }



        //creates a set of all edges on the map
        List<edge> map = new List<edge>();
        foreach(point a in points)
        {
            foreach(point b in points)
            {
                if (a != b)
                {
                    map.Add(new edge(a,b));
                }
            }
        }

        //finds the weight on all edges
        foreach(edge e in map)
        {
            e.AssignWeight();
        }

        //creates a minimum spanning tree
        map.Sort((x, y) => x.dist.CompareTo(y.dist));
        List<edge> patrol = new List<edge>();
        int totalEdges = 0;
        int index = 0;
        while(totalEdges < points.Count -1)
        {

            if (wouldMakeCyclical(map[index]))
            {
                totalEdges++;
                patrol.Add(map[index]);
            }

            index++;
            if (index >= map.Count)
            {
                Debug.LogWarning("Failed to create MST");
                break;
            }

        }

        
        foreach (edge e in patrol)
        {
            e.showEdge();
        }
        




    }
}
