﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalObjective : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameManager.instance.numObjectives <= 0)
            {
                Debug.Log("WIN");
            }
        }
    }
}