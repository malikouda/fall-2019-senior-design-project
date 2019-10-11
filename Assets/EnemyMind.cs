using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMind : MonoBehaviour
{

    public enum STATES {PATROL,INVES,ALERT}
    private guardMovement move;
    private STATES state;
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

    public void ChangeState(STATES newstate)
    {
        state = newstate;
    }

    public void Investigate(Vector3 position)
    {
        move.investigate(position);
    }
}
