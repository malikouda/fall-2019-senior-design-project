using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeArt : MonoBehaviour
{
    public GameObject[] art;
    void Start() {
        int randomArtIndex = Random.RandomRange(0, art.Length);
        GameObject randomArt = Instantiate(art[randomArtIndex]) as GameObject;
        randomArt.transform.position = transform.position;
        randomArt.transform.parent = transform;
    }
}
