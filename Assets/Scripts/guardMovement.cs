﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class guardMovement : MonoBehaviour {


    public float threshold;
    public float minWaitTime;
    public float MaxWaitTime;
    private EnemyMind mind;
    private List<Vector3> patrol;
    private int index;
    private NavMeshAgent mAgent;
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

        if (searching)
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
                if (searching)
                {
                    mind.ChangeState(EnemyMind.STATES.PATROL);
                    searching = false;
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
        searching = true;
        searchTarget = target;
    }
}
