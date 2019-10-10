using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInfo : MonoBehaviour
{
    public int roles;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void assignRole(int role)
    {
        roles += roles;
    }
}


