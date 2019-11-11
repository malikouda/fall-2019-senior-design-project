﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMind : MonoBehaviour
{

    public enum STATES {PATROL,INVES,ALERT,WAITING}
    [Tooltip("Minimum amount of time the guard will wait at every waypoint")]
    public float minWaitTime;
    [Tooltip("Max amount of time the guard will wait at every waypoint")]
    public float MaxWaitTime;
    public float totalAlertTime;

    private Character target;
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
                if (move.isWithinThreshold())
                {
                    state = STATES.WAITING;
                    Invoke("goToNextPatrol", Random.Range(minWaitTime, MaxWaitTime));
                }
                break;
            case STATES.INVES:
                if (move.isWithinThreshold())
                {
                    //TODO: MAKE GUARD ACTUALLY INVESTIGATE
                    state = STATES.PATROL;
                }
                break;
            case STATES.ALERT:
                move.goToPosition(target.gameObject.transform.position);
                if (move.hasCaughtPlayer(target.gameObject))
                {
                    GameManager.instance.catchPlayer();
                    target.immobilize();
                    state = STATES.PATROL;
                    target = null;
                }
                break;
        }
    }

    //manually change states
    public void ChangeState(STATES newstate)
    {
        state = newstate;
    }
     
    //Sets the agent to alert
    public void alert (Character target)
    {
        move.increaseSpeed();
        state = STATES.ALERT;
        this.target = target;

    }

    //go to next patrol point
    private void goToNextPatrol()
    {
        Debug.Log("Go to next Patrol");
        //State has changed since waiting, do not go back to patrolling
        if (state != STATES.WAITING)
            return;
        state = STATES.PATROL;
        move.goToNextPatrol();
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
            float timeToReach = Vector3.Distance(target.transform.position, transform.position) / relativeSpeed; 

        }
        else
        {
            return target.transform.position;
        }

        return Vector3.zero;
    }

    //has the agent investigate a given position
    public void Investigate(Vector3 position)
    {
        state = STATES.INVES;
        move.goToPosition(position);
    }
}
