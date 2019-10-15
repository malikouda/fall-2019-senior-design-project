using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class patrolCreation : MonoBehaviour
{
    public static List<node> nodeGraph;
    //public GameObject[] rooms;
    public List<Vector3> randomPoints;

<<<<<<< HEAD
    public static patrolCreation instance;

    private List<Vector3> points;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public static void createPatrols()
    {
        instance.makePatrols();
    }


    public void makePatrols()
    {
        points = new List<Vector3>();
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("room");
        foreach(GameObject g in rooms)
        {
            List<Vector3> cells = new List<Vector3>();
            foreach (Transform c in g.GetComponentInChildren<GameObject>().transform)
            {
                if (c.gameObject.tag == "cell")
                {
                    cells.Add(c.gameObject.transform.position);
=======
    public class node
    { 


        Dictionary <node, float> edges = new Dictionary<node,float>();
        public Vector3 pos;
        public node (Vector3 pos)
        {
            this.pos = pos;
        }

        public void calculateDistance(node other)
        {
            if (other != this)
            {
                float dist = Vector3.Distance(pos, other.pos);
                edges.Add(other, dist);
            }
            return;

        }

        public float getDistance(node other)
        {
            try
            {
                return edges[other];
            }catch
            {
                calculateDistance(other);
                return edges[other];
            }
        }

        public node getClosestNode()
        {
            float minval = 0;
            node minNode = null;
            foreach(node n in edges.Keys)
            {
                if (edges[n] < minval)
                {
                    minval = edges[n];
                    minNode = n;
                }
            }
            return minNode;
        }


    }

    public class path
    {
        public float distance;
        public List<node> nodes;

        public path()
        {
            this.nodes = new List<node>();
        }

        public void addNode(node other,node curNode)
        {
            nodes.Add(other);
            distance += curNode.getDistance(other);
        }

        public void removeNode(node other,node curNode)
        {
            nodes.Remove(other);
            distance -= curNode.getDistance(other);
        }
    }

    path makePath(List<node> remainingNodes, node curNode, path curPath)
    {
        if (remainingNodes.Count == 0)
        {
            return curPath;
        }

        path bestPath = null;
        float minPath = Mathf.Infinity;
        foreach(node n in remainingNodes)
        {
            List<node> nextNodes = remainingNodes;
            nextNodes.Remove(n);
            curPath.addNode(n,curNode);
            path nextPath = makePath(nextNodes, n, curPath);
            if (nextPath.distance < minPath)
            {
                bestPath = nextPath; 
            }
            curPath.removeNode(n,curNode);
        }

        return bestPath;
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform[] rooms;
        if (nodeGraph == null)
        {
            nodeGraph = new List<node>();
            rooms = GetComponentsInChildren<Transform>();
            foreach (Transform child in transform)
            {
                MazeCell[] m = child.gameObject.GetComponentsInChildren<MazeCell>();
                if (m.Length == 0)
                {
                    continue;
                }
                IntVector2 v = m[Random.Range(0, m.Length - 1)].coordinates;
                nodeGraph.Add(new node(new Vector3(v.x, 1, v.z)));
            }

            foreach (Vector3 v in randomPoints)
            {
                node curNode = new node(v);
                foreach(node n in nodeGraph)
                {
                    n.calculateDistance(curNode);
>>>>>>> ad8fbbf3bcfe1298da0fb8152f99162cd00d756e
                }
            }
            points.Add(cells[Random.Range(0, cells.Count - 1)]);
        }
<<<<<<< HEAD
=======
        
        path patrol = makePath(nodeGraph, nodeGraph[0], new path());
>>>>>>> ad8fbbf3bcfe1298da0fb8152f99162cd00d756e

        node prevNode = patrol.nodes[0];
        foreach(node n in patrol.nodes)
        {
            Debug.DrawLine(n.pos, prevNode.pos);
            prevNode = n;

<<<<<<< HEAD

=======
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
>>>>>>> ad8fbbf3bcfe1298da0fb8152f99162cd00d756e
    }

}
