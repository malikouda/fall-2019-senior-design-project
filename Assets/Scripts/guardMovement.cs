using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class guardMovement : MonoBehaviour {


    public float threshold;
    [Tooltip("Minimum amount of time the guard will wait at every waypoint")]
    public float minWaitTime;
    [Tooltip("Max amount of time the guard will wait at every waypoint")]
    public float MaxWaitTime;

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
        randomWaitTime = Random.Range(minWaitTime, MaxWaitTime);
        currentWaitTime = 0;
        mind = GetComponent<EnemyMind>();
        searchTarget = Vector3.zero;
        mAgent = gameObject.GetComponent<NavMeshAgent>();
        GameObject current = gameObject;
    }

    public void assignPatrol(List<Vector3> newPatrol)
    {
        patrol = new List<Vector3>();
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
    // Update is called once per frame
    void Update()
    {

        if (mind.state != EnemyMind.STATES.PATROL)
        { 
            mAgent.destination = searchTarget;
        }

        if (mAgent.remainingDistance < threshold) 
        {
            currentWaitTime += Time.deltaTime;
            if (currentWaitTime > randomWaitTime)
            {
                randomWaitTime = Random.Range(minWaitTime, MaxWaitTime);
                currentWaitTime = 0;
                index = (index + 1) % patrol.Count;
                if (mind.state == EnemyMind.STATES.INVES)
                {
                    mind.ChangeState(EnemyMind.STATES.PATROL);
                    mAgent.destination = patrol[index];
                    return;
                }
                Debug.Log(index);

                mAgent.destination = patrol[index];
            }
        }

    }

    public void investigate(Vector3 target)
    {
        searchTarget = target;
    }
}
