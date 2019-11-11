using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class guardMovement : MonoBehaviour {

    [Tooltip("How far from the waypoint the guard has to be to move on")]
    public float threshold;
    [Tooltip("How fast the guard moves when they are alerted")]
    public float alertSpeed;
    public float catchDistance;
    //state controller
    private EnemyMind mind;
    //the speed the guard normally moves
    private float normalSpeed;
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
    void Awake()
    {
        mind = GetComponent<EnemyMind>();
        searchTarget = Vector3.zero;
        mAgent = gameObject.GetComponent<NavMeshAgent>();
        normalSpeed = mAgent.speed;

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

    public bool hasCaughtPlayer(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < catchDistance)
        {
            decreaseSpeed();
            return true;
        }
        return false;
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
    
    public void increaseSpeed()
    {
        mAgent.speed = alertSpeed;
        mAgent.autoBraking = false;
    }

    public void decreaseSpeed()
    {
        mAgent.speed = normalSpeed;
        mAgent.autoBraking = true;
    }

    public void resetSpeed()
    {
        mAgent.speed = normalSpeed;
        mAgent.autoBraking = true;

    }
}
