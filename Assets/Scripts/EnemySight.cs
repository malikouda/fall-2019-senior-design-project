using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySight : MonoBehaviour
{
    public Text debugText;

    [Tooltip("How long the player can be seen before being caught")]
    public float hideTime;
    [Tooltip("Multiplier for how long it takes the guard to forget about the player")]
    public float hideCooldown;
    [Tooltip("Show the sightlines")]
    public bool drawSight;
    public float distance;
    [Tooltip("Change the angle of the cone of vision")]
    [Range(0,1)]
    public float visionThreshold;


    private float hideTimeMax;
    // Start is called before the first frame update
    void Start()
    {
        hideTimeMax = hideTime;
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if the player is in the enemies line of sight
        bool seen = false;
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Vector3 dir = transform.position - p.transform.position;
            dir = transform.InverseTransformDirection(dir);
            if (Mathf.Abs(dir.x) <= visionThreshold * dir.magnitude)
            {
                Debug.DrawRay(transform.position, -dir, Color.black);
                seen = true;
                break;
            }
        }

        //has the guard caught the player?
        if (seen)
        {
            hideTime -= Time.deltaTime;
            if (hideTime <= 0)
            {
                //Game over
                
            }
        }else
        {
            hideTime += Time.deltaTime / hideCooldown;
            hideTime = Mathf.Clamp(hideTime, 0, hideTimeMax);
            
        }

        debugText.text = ""+ hideTime;
    }

    private void OnDrawGizmos()
    {
        if(drawSight)
        {
            Debug.DrawRay(transform.position, transform.InverseTransformDirection(new Vector3(0 + visionThreshold, 0, .5f) * distance), Color.red);
            Debug.DrawRay(transform.position, transform.InverseTransformDirection(new Vector3(0 - visionThreshold, 0, .5f) * distance), Color.red);

        }

    }
}
