using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class patrolCreation : MonoBehaviour
{

    public int resolution;

    public float XCoord;
    public float ZCoord;

    public static List<Vector3> points;


    // Start is called before the first frame update
    void Start() {
        if (points == null) 
        {
            points = new List<Vector3>();
            for (float x = -XCoord; x <= XCoord; x += ((XCoord) / resolution)) {
                for (float z = -ZCoord; z <= ZCoord; z += ((ZCoord) / resolution)) {
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(x, 10, z), Vector3.down, out hit)) {
                        if (hit.collider.gameObject.tag == "Ground") {
                            points.Add(hit.point);
                        }
                    }
                }

            }

            List<Vector3> maxPoints = new List<Vector3>();
            for (int i = 0; i < points.Count; i++) {
                int index = 0;
                float maxDistance = 0;
                for (int x = 0; x < points.Count; x++) 
                {

                    if (!Physics.Linecast(points[i], points[x])) 
                    {
                        float dist = Vector3.Distance(points[i], points[x]);
                        if (dist > maxDistance) 
                        {
                            maxDistance = dist;
                        }else 
                        {
                            points.RemoveAt(index);
                        }
                    }
                    maxPoints.Add(new Vector3(points[i].x + points[index].x / 2, points[i].y + points[index].y / 2, points[i].z + points[index].z / 2));
                    points.RemoveAt(i);
                }
                
            }

            foreach (Vector3 point in maxPoints) 
            {
                Debug.DrawRay(point, Vector3.up,Color.blue,10);
            }
        }
       


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(XCoord, 10, ZCoord));
    }

}
