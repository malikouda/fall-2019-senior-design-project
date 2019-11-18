using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testing : MonoBehaviour
{
    public GameObject screen;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        screen.SetActive(true);
        anim.SetTrigger("Win");
    }
}
