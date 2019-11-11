using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EnemySight : MonoBehaviour
{
    public Sprite sightSprite;
    [Tooltip("How close the guard will notice players even if they are behind them")]
    public float SenseDistance;
    [Tooltip("How long the player can be seen before being caught")]
    public float hideTime;
    [Tooltip("Multiplier for how long it takes the guard to forget about the player")]
    public float hideCooldown;
    [Tooltip("Show the sightlines")]
    public bool drawSight;
    public float distance;
    [Tooltip("Change the angle of the cone of vision")]
    [Range(0,180)]
    public float visionThreshold;

    private Image sightImage;
    private EnemyMind mind;
    private float hideTimeMax;
    private List<Character> players;
    // Start is called before the first frame update
    void Start()
    {
        mind = GetComponent<EnemyMind>();
        sightImage = GetComponentInChildren<Image>();
        hideTimeMax = hideTime;
        players = new List<Character>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            Character c = g.GetComponent<Character>();
            if (c != null)
                players.Add(c);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Don't check for sight if the enemy is already alerted
        if (mind.state != EnemyMind.STATES.ALERT)
        {
            //check to see if the player is in the enemies line of sight
            bool seen = false;
            foreach (Character p in players)
            {
                //If the player is caught, no need to check them
                if (!p.isActivated)
                    continue;
                //if the player is within the cone of vision or they are too close to the guard, they can be seen
                Vector3 dir = p.transform.position - transform.position;
                float angle = Vector3.Angle(dir, transform.forward);
                float distance = Vector3.Distance(p.gameObject.transform.position, transform.position);
                if (angle <= visionThreshold/2 || distance < SenseDistance)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, dir.normalized, out hit))
                    { 
                        if(hit.collider.tag == "Player")
                        {
                            hideTime -= Time.deltaTime + 1/distance;

                            //If the player has been spotted too much
                            if (hideTime <= 0)
                            {
                                //Alert the guard
                                mind.alert(p);
                                hideTime = hideTimeMax;
                            }
                            if (hideTime <= hideTimeMax/2)
                            {
                                //Or investigate if not fully caught
                                mind.Investigate(p.gameObject.transform.position); 
                            }

                        }

                    }
                    seen = true;
                    break;
                }
            }

            //if there is no player in sight
            if (!seen)
            {
                hideTime += Time.deltaTime / hideCooldown;
                hideTime = Mathf.Clamp(hideTime, 0, hideTimeMax);

            }

            sightImage.color = Color.Lerp(Color.red, Color.clear, hideTime / hideTimeMax);

        }


    }

    private void OnDrawGizmos()
    {
        if(drawSight)
        {
            Gizmos.DrawRay(transform.position, new Vector3 (transform.forward.x - visionThreshold, transform.forward.y,transform.forward.z) * distance);
            Gizmos.DrawRay(transform.position, new Vector3(transform.forward.x + visionThreshold, transform.forward.y, transform.forward.z) * distance);
            Gizmos.DrawWireSphere(transform.position, SenseDistance);

        }

    }
}
