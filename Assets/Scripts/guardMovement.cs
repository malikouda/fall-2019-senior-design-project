using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class guardMovement : MonoBehaviour {


    public float threshold;


    //state controller
    private EnemyMind mind;
    //patrol waypoints
    private List<Vector3> patrol;
    //current patrol destination
    private int index;
    //the agent on this object
    [HideInInspector]
    public NavMeshAgent mAgent;
    private int direction = 1;
    private bool searching;
    private Vector3 searchTarget;
    private float randomWaitTime;
    private float currentWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        mind = GetComponent<EnemyMind>();
        searchTarget = Vector3.zero;
        mAgent = gameObject.GetComponent<NavMeshAgent>();
        GameObject current = gameObject;

    }

    public void assignPatrol(List<Vector3> newPatrol)
    {
        patrol = newPatrol;
    }

    public void assignPatrol(List<Vector3> newPatrol, Color DebugColor)
    {
        patrol = new List<Vector3>();
        patrol = newPatrol;

        Vector3 last = newPatrol[0];
        foreach (Vector3 next in newPatrol)
        {
            Debug.DrawLine(last, next, DebugColor, 100);
            last = next;
        }
    }

    public bool isWithinThreshold()
    {
        return mAgent.remainingDistance < threshold;
    }

    public void goToNextPatrol()
    {
        index = (index + 1) % patrol.Count;
        mAgent.destination = patrol[index];
    }
     
    public void goToPosition (Vector3 target)
    {
        mAgent.destination = target;
    }
}
