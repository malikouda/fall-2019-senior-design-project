using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class guardMovement : MonoBehaviour {

    [Tooltip("How far from the waypoint the guard has to be to move on")]
    public float threshold;
    [Tooltip("How fast the guard moves when they are alerted")]
    public float alertSpeed;
    [Tooltip("How far the guard has to be to catch you")]
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
    //Is searching for player
    private bool searching;
    //The target they are looking for
    private Vector3 searchTarget;
    //How long the guard needs to wait for
    private float randomWaitTime;
    //How long they have currently waited for
    private float currentWaitTime;

    // Start is called before the first frame update
    void Awake()
    {
        mind = GetComponent<EnemyMind>();
        searchTarget = Vector3.zero;
        mAgent = gameObject.GetComponent<NavMeshAgent>();
        normalSpeed = mAgent.speed;

    }

    
    //Assign the guards patrol
    public void assignPatrol(List<Vector3> newPatrol)
    {
        patrol = newPatrol;
    }

    //Assign the guards patrol and shows the path (For testing)
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

    //Is within the threshold of their destination
    public bool isWithinThreshold()
    {
        return mAgent.remainingDistance < threshold;
    }

    //Has the guard cause a player
    public bool hasCaughtPlayer(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < catchDistance)
        {
            decreaseSpeed();
            return true;
        }
        return false;
    }

    //Continue on to the next patrol point
    public void goToNextPatrol()
    {
        index = (index + 1) % patrol.Count;
        mAgent.destination = patrol[index];
    }
     
    //Manually set navmesh destination
    public void goToPosition (Vector3 target)
    {
        mAgent.destination = target;
    }
    
    //Speed up when chasing
    public void increaseSpeed()
    {
        mAgent.speed = alertSpeed;
        mAgent.autoBraking = false;
    }

    //Return to normal speed
    public void decreaseSpeed()
    {
        mAgent.speed = normalSpeed;
        mAgent.autoBraking = true;
    }
}
