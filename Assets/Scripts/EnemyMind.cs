using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMind : MonoBehaviour
{

    public enum STATES {PATROL,INVES,ALERT,WAITING}
    [Tooltip("Minimum amount of time the guard will wait at every waypoint")]
    public float minWaitTime;
    [Tooltip("Max amount of time the guard will wait at every waypoint")]
    public float MaxWaitTime;
    [Tooltip("How long without sighting before the guard gives up")]
    public float totalAlertTime;
    [Tooltip("How often to check if the guard is stuck")]
    public float stuckWaitTime;
    [Tooltip("The state the guard is in")]
    public STATES state;

    public float currentAlertTime;
    private Character target;
    private guardMovement move;
    private EnemySight sight;
    private bool isStuck;
    private float currentWaitTime;
    private float nextWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        isStuck = false;
        sight = GetComponent<EnemySight>();
        move = GetComponent<guardMovement>();
        state = 0;
        StartCoroutine(isGuardStuck());
    }

    // Update is called once per frame
    void Update()
    {

        //Simple state machine
        switch (state)
        {
            case STATES.WAITING:
                currentWaitTime += Time.deltaTime;
                if (currentWaitTime > nextWaitTime)
                {
                    goToNextPatrol();
                }
                break;
            //The guard is patrolling
            case STATES.PATROL:
                //if the guard is at the next patrol point, go to the next one, or if they're stuck
                if (move.isWithinThreshold() || isStuck)
                {
                    state = STATES.WAITING;
                    nextWaitTime = Random.Range(minWaitTime, MaxWaitTime);
                    currentWaitTime = 0;
                }
                break;
            //The guard has seen a player partially
            case STATES.INVES:
                //The guard is at the last known location, return to patrol
                if (move.isWithinThreshold())
                {
                    //TODO: MAKE GUARD ACTUALLY INVESTIGATE
                    state = STATES.WAITING;
                    currentWaitTime = 0;
                }
                break;
            //The guard has caught a player
            case STATES.ALERT:

                //if the target has already been caught
                if (!target.isActivated)
                {
                    //go back to normal
                    GameManager.instance.evadeGuard();
                    state = STATES.PATROL;
                    target = null;
                    break;
                }

                //Go to position
                move.goToPosition(target.gameObject.transform.position);
                
                //if guard can't see the target
                if (!sight.canSeeTarget(target.gameObject))
                {
                    //increment the timer
                    currentAlertTime += Time.deltaTime;
                    //if it's been too long since they last saw the player
                    if (currentAlertTime > totalAlertTime)
                    {
                        //the player has escaped
                        GameManager.instance.evadeGuard();
                        currentAlertTime = 0;
                        state = STATES.PATROL;
                        target = null;
                        break;
                    }
                }else
                {
                    currentAlertTime = 0;
                }
                
                //If the player has been caught
                if (move.hasCaughtPlayer(target.gameObject))
                {
                    //Immobilize the character and update the game manager
                    GameManager.instance.catchPlayer();
                    GameManager.instance.evadeGuard();
                    target.immobilize();
                    state = STATES.PATROL;
                    target = null;
                }
                break;
        }
    }
     
    //Sets the agent to alert
    public void alert (Character target)
    {
        //alert gamemanager to change music
        GameManager.instance.alertGuard();
        
        move.increaseSpeed();
        state = STATES.ALERT;
        this.target = target;

    }

    //go to next patrol point
    private void goToNextPatrol()
    {

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

    //If the guard is stuck
    private IEnumerator isGuardStuck()
    {
        while (true)
        {
            Vector3 lastPos = transform.position;
            yield return new WaitForSeconds(5f);
            if (Vector3.Distance(lastPos, transform.position) < .1f)
            {
                if (state == STATES.PATROL)
                    isStuck = true;
            }
            else
            {
                isStuck = false;
            }
        }
    }
}
