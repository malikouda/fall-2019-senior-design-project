using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalObjective : MonoBehaviour
{
    public GameObject lasers;
    public static FinalObjective instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void disableLasers()
    {
        lasers.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameManager.instance.numObjectives <= 0)
            {
                GameManager.instance.wonGame();
            }
        }
    }
}
