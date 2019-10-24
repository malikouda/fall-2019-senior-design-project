using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMind : MonoBehaviour
{

    public enum STATES {PATROL,INVES,ALERT}

    public float totalAlertTime;

    private GameObject target;
    private guardMovement move;
    public STATES state;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<guardMovement>();
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATES.PATROL:
                break;
            case STATES.INVES:
                break;
            case STATES.ALERT:
                break;
        }
    }

    //manually change states
    public void ChangeState(STATES newstate)
    {
        state = newstate;
    }
     
    //Sets the agent to alert
    public void alert (GameObject target)
    {
        state = STATES.ALERT;
        this.target = target;

    }

    //returns where the guard should go while pursuing
    private Vector3 pursue()
    {
        Rigidbody targetRigid = target.GetComponent<Rigidbody>();
        if (targetRigid is Rigidbody)
        {
            float relativeSpeed = move.mAgent.speed - targetRigid.velocity.magnitude;
            //Target is faster than the agent, just chase it
            if (relativeSpeed <= 0)
            {
                return target.transform.position;
            }
            float timeToReach = Vector3.Distance(target.transform.position,transform.position)/relativeSpeed
        }else
        {
            return target.transform.position;
        }

        return Vector3.zero;
    }

    //has the agent investigate a given position
    public void Investigate(Vector3 position)
    {
        move.investigate(position);
    }
}
