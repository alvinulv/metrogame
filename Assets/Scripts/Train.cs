using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] GameObject Route;
    [SerializeField] Transform nextWaypoint;
    [SerializeField] GameObject nextStop;
    [SerializeField] List<GameObject> passengers = new List<GameObject>();
    Routelogic Routelogic;
    [SerializeField] int index;
    [SerializeField] bool loop;
    bool reverse;
    public float speed = 0.03f;
    public float mindist = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Routelogic = Route.GetComponent<Routelogic>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (nextWaypoint.position - transform.position).normalized * speed;
        if ((nextWaypoint.position - transform.position).magnitude < mindist)
        {
            Debug.Log("Waypoint reached");
            if (!reverse)
            {
                index++;
                if (index > Routelogic.routeWaypoints.Count)
                {
                    if (loop)
                        index = 0;
                    else
                    {
                        reverse = true;
                        index--;
                    }
                }
            }
            else
            {
                index--;
                if (index < 0)
                {
                    reverse = false;
                    index = 0;
                }
            }
            nextWaypoint = Routelogic.routeWaypoints[index].transform;
        }
            
        if ((nextStop.transform.position - transform.position).magnitude < mindist)
            ReachedStop(nextStop.tag);
    }
    void ReachedStop(string type)
    {
        for (int i = 0; i < passengers.Count; i++)
        {
            if (passengers[i].CompareTag(type))
            {
                Destroy(passengers[i]);
                passengers.RemoveAt(i);
                i--;
            }
        }
    }
}
