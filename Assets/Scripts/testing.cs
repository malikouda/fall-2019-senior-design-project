using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.UI;

public class testing : MonoBehaviour
{
    public GameObject screen;
    public Animator anim;
    public string trigger;
    // Start is called before the first frame update
    void Start()
    {
        
        screen.SetActive(true);
        anim.SetTrigger(trigger);
    }
}
