using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EnemySight : MonoBehaviour
{
    public Sprite sightSprite;
    private Image sightImage;
    private EnemyMind mind;
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


    private float hideTimeMax;
    // Start is called before the first frame update
    void Start()
    {
        mind = GetComponent<EnemyMind>();
        sightImage = GetComponentInChildren<Image>();
        hideTimeMax = hideTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Don't check for sight if the enemy is already alerted
        if (mind.state != EnemyMind.STATES.ALERT)
        {
            //check to see if the player is in the enemies line of sight
                    bool seen = false;
                    foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        Vector3 dir = p.transform.position - transform.position;
                        float angle = Vector3.Angle(dir, transform.forward);
                        if (angle <= visionThreshold/2)
                        {
                            RaycastHit hit;
                            if (Physics.Raycast(transform.position, dir.normalized, out hit))
                            {
                                Debug.Log(hit.collider.gameObject);
                                if(hit.collider.tag == "Player")
                                {
                                    hideTime -= Time.deltaTime + 1/Vector3.Distance(transform.position,p.transform.position);

                                    if (hideTime <= 0)
                                    {
                                        mind.alert(p.gameObject.GetComponent<Character>());
                                    }
                                    if (hideTime >= hideTimeMax/2)
                                    {
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
            Debug.DrawRay(transform.position, new Vector3 (transform.forward.x - visionThreshold, transform.forward.y,transform.forward.z) * distance, Color.red);
            Debug.DrawRay(transform.position, new Vector3(transform.forward.x + visionThreshold, transform.forward.y, transform.forward.z) * distance, Color.red);


        }

    }
}
