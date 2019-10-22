using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRandomArtAsset : MonoBehaviour
{
    public GameObject[] possibleArt;
    public GameObject point;
    void Start()
    {
        if (Random.Range(0f,100f) > 90f)
        {
            Instantiate(possibleArt[Random.Range(0, possibleArt.Length)],point.transform.position,Quaternion.identity);

        }else
        {
            Destroy(point);
        }
    }

}
