using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class patrolCreation : MonoBehaviour
{

    public static patrolCreation instance;

    private List<Vector3> points;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public static void createPatrols()
    {
        instance.makePatrols();
    }


    public void makePatrols()
    {
        points = new List<Vector3>();
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("room");
        foreach(GameObject g in rooms)
        {
            List<Vector3> cells = new List<Vector3>();
            foreach (Transform c in g.GetComponentInChildren<GameObject>().transform)
            {
                if (c.gameObject.tag == "cell")
                {
                    cells.Add(c.gameObject.transform.position);
                }
            }
            points.Add(cells[Random.Range(0, cells.Count - 1)]);
        }



    }

}
