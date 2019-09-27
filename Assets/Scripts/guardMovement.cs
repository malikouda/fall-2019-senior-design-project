using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class guardMovement : MonoBehaviour {


    public float threshold;

    private List<GameObject> waypoints;
    private List<Vector3> patrol;
    private int index;
    private NavMeshAgent mAgent;
    private int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        mAgent = gameObject.GetComponent<NavMeshAgent>();
        patrol = new List<Vector3>();
        waypoints = new List<GameObject>();
        foreach (GameObject waypoint in GameObject.FindGameObjectsWithTag("Waypoint")) 
        {
            waypoints.Add(waypoint);
        }
        GameObject current = gameObject;
        int total = waypoints.Count;
        while (patrol.Count < total) {
            GameObject next = findClosestPoint(current);
            patrol.Add(next.transform.position);
            waypoints.Remove(current);
            current = next;
        }

        Vector3 lastpoint = transform.position;
        foreach (Vector3 point in patrol) 
        {
            Debug.DrawLine(lastpoint, point,Color.red,100);
            lastpoint = point;
        }
    }

    GameObject findClosestPoint(GameObject current) 
    {
        float minDist = Mathf.Infinity;
        GameObject point = null;
        foreach (GameObject waypoint in waypoints) 
        {
            if (waypoint != current) 
            {
                float dist = Vector3.Distance(current.transform.position, waypoint.transform.position);
                if (dist < minDist) {
                    minDist = dist;
                    point = waypoint;
                }
            }

        }

        return point;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (mAgent.remainingDistance < threshold) 
        {
            index += 1 * direction;
            if (index >= patrol.Count) 
            {
                index = patrol.Count - 1;
                direction = -1;
            }

            if (index < 0) 
            {
                index = 0;
                direction = 1;
            }

            
        }
        mAgent.destination = patrol[index];
    }
}
