using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeArt : MonoBehaviour
{
    public Art[] art;
    void Start() {
        int randomArtIndex = Random.RandomRange(0, art.Length);
        Art randomArt = Instantiate(art[randomArtIndex]) as Art;
        randomArt.transform.position = transform.position;
        randomArt.transform.parent = transform;
        if (randomArt.randomRotation) {
            if (randomArt.specifyAngles) {
                int idx = Random.RandomRange(0, randomArt.angles.Length);
                randomArt.transform.rotation = Quaternion.Euler(new Vector3(0, randomArt.angles[idx], 0));
            } else {
                randomArt.transform.rotation = Quaternion.Euler(new Vector3(0, Random.RandomRange(0, 360), 0));
            }
        }
    }
}
