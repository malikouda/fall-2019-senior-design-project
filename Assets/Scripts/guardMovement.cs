using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class guardMovement : MonoBehaviour {


    public float threshold;
    
    private EnemyMind mind;
    private List<Vector3> patrol;
    private int index;
    private NavMeshAgent mAgent;
    private int direction = 1;
    private bool searching;
    private Vector3 searchTarget;
    // Start is called before the first frame update
    void Start()
    {
        mind = GetComponent<EnemyMind>();
        searchTarget = Vector3.zero;
        mAgent = gameObject.GetComponent<NavMeshAgent>();
        GameObject current = gameObject;
        Vector3 lastpoint = transform.position;
    }

    public void assignPatrol(List<Vector3> newPatrol)
    {
        patrol = new List<Vector3>();
        patrol = newPatrol;
    }
    
    // Update is called once per frame
    void Update()
    {

        if (searching)
        {
            mAgent.destination = searchTarget;
        }

        if (mAgent.remainingDistance < threshold) 
        {
            if (searching)
            {
                mind.ChangeState(EnemyMind.STATES.PATROL);
                searching = false;
                mAgent.destination = patrol[index];
                return;
            }
            Debug.Log(index);
            index = (index + 1) % patrol.Count;
            mAgent.destination = patrol[index];
        }

    }

    public void investigate(Vector3 target)
    {
        searching = true;
        searchTarget = target;
    }
}
