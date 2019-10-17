using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
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
        foreach (GameObject g in rooms)
        {
            List<Vector3> cells = new List<Vector3>();
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag == "cell")
                {
                    cells.Add(child.gameObject.transform.position);
                    child.gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
                }
            }
            if (cells.Count != 0)
            {
                points.Add(cells[Random.Range(0, cells.Count)]);

            }
        }

        GameObject.Find("Guard").GetComponent<NavMeshAgent>().SetDestination(new Vector3(4,0,4));



    }
}
