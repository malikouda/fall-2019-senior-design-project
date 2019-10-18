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
    private const float equalityThreshold = 3;
    private List<point> points;

    
    public class point
    {
        public Vector3 pos;
        public List<point> adj;
        public point (Vector3 pos)
        {
            adj = new List<point>();
            this.pos = pos;
        }

        public void assignEdge (edge e)
        {
            if (e.start != this)
            {
                adj.Add(e.start);
            }
            if (e.end != this)
            {
                adj.Add(e.end);
            }
        }

        public void removeEdge (edge e)
        {
            if (e.start != this)
            {
                adj.Remove(e.start);
            }
            if (e.end != this)
            {
                adj.Remove(e.end);
            }
        }

        public List<point> getAdj()
        {
            return adj;
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

    //Would adding  the edge cause the graph to be cyclical
    private bool wouldMakeCyclical(edge e, List<point> points)
    {
        e.addtoTree();
        List<point> v = new List<point>();
        Stack<point> nextPoints = new Stack<point>();
        foreach (point p in points)
        {
            //if the point hasn't already been checked
            if (!v.Contains(p))
            {
                nextPoints.Push(p);
                if (isCyclical(nextPoints, v, p, out v))
                {
                    e.removeFromTree();
                    return true;
                }
            }

        }
        return false;
    }

    //is this graph cyclical
    private bool isCyclical(Stack<point> current, List<point> visited, point parent, out List<point> newVisted)
    {
        if (current.Count == 0)
        {
            newVisted = new List<point>(visited);
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
                    newVisted = new List<point>(visited);
                    return true;
                }

            }else
            {
                current.Push(p);
                if (isCyclical(current, visited, nextPoint,out newVisted))
                {
                    newVisted = new List<point>(visited);
                    return true;
                }
            }

        }
        newVisted = new List<point>(visited);
        return false;
    }

    //creates patrol patterns by selecting random points in each room and connecting them
    private void makePatrols()
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
        while(totalEdges <= points.Count -1)
        {

            if (!wouldMakeCyclical(map[index],points))
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

        AssignRoutes(patrol);




    }
    
    private edge getEdge(point a, point b, List<edge> graph)
    {
        foreach(edge e in graph)
        {
            if ((e.start == a || e.end == a) && (e.start == b || e.end == b))
            {
                return e;
            }
        }

        return null;
    }

    private void AssignRoutes(List<edge> totalPatrol)
    {
        
    }

    private void subdivideGraph (List<edge> sourceGraph)
    {
        List<edge> tempGraph = new List<edge>(sourceGraph);
        Dictionary<edge, float> possibleGraphs = new Dictionary<edge, float>();
        foreach(edge e in tempGraph)
        {
            e.removeFromTree();
            Stack <point> aStack = new Stack<point>();
            aStack.Push(e.start);
            Stack<point> bStack = new Stack<point>();
            bStack.Push(e.end);
            float aDist = graphTraversal(aStack, new List<point>(), 0f,sourceGraph);
            float bDist = graphTraversal(bStack, new List<point>(), 0f,sourceGraph);

            

            possibleGraphs.Add(e, Mathf.Abs(aDist - bDist));
            e.addtoTree();
        }

    }

    float graphTraversal(Stack<point> points, List<point> visited, float totalDist,List<edge> sourceGraph)
    {
        if (points.Count == 0)
        {
            return totalDist;
        }

        point current = points.Pop();
        visited.Add(current);
        foreach (point p in current.adj)
        {
            if (!visited.Contains(p))
            {
                totalDist += getEdge(p, current,sourceGraph).dist;
                points.Push(p);
            }
        }

        return totalDist;
    }

}
