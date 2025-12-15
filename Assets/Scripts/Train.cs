using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] GameObject Route;
    [SerializeField] Vector3 nextWaypoint;
    [SerializeField] GameObject nextStop;
    [SerializeField] List<GameObject> passengers = new List<GameObject>();
    Routelogic Routelogic;
    [SerializeField] int index;
    [SerializeField] int maxPassengers = 6;
    [SerializeField] float startx = -0.4f;
    [SerializeField] float incrementx = 0.3f;
    [SerializeField] float starty = 0.2f;
    [SerializeField] float incrementy = -0.3f;
    [SerializeField] GameObject squarePassenger;
    [SerializeField] GameObject circlePassenger;
    [SerializeField] GameObject trianglePassenger;
    Stations station;
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
        transform.position = transform.position + (nextWaypoint - transform.position).normalized * speed;
        if ((nextWaypoint - transform.position).magnitude < mindist)
        {
            //Debug.Log("Waypoint reached");
            if (!reverse)
            {
                index++;
                if (index > Routelogic.rWp.Count)
                {
                    if (Routelogic.isLoop)
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
            nextWaypoint = Routelogic.rWp[index];
        }
            
        if ((nextStop.transform.position - transform.position).magnitude < mindist)
            ReachedStop(nextStop.tag);
    }
    void ReachedStop(string type)
    {
        station = nextStop.GetComponent<Stations>();
        //removing passengers
        for (int i = 0; i < passengers.Count; i++)
        {
            if (passengers[i].CompareTag(type))
            {
                Destroy(passengers[i]);
                passengers.RemoveAt(i);
                i--;
            }
        }
        //adding passengers
        for (int i = 0;i < station.people.Length;i++)
        {
            switch (station.people[i])
            {
                case "Square": newPassenger(squarePassenger); break;
                case "Circle": newPassenger(circlePassenger); break;
                case "Triangle": newPassenger(trianglePassenger); break;
                case "null":break;
                default: break;
            }
                
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Square"))
        {
            if (!passengers.Contains(collision.gameObject))
            {
                newPassenger(collision.gameObject);
                /*collision.gameObject.transform.parent = transform;
                collision.gameObject.transform.position = transform.position + new Vector3(startx + (incrementx * passengers.Count), starty, -1);
                passengers.Add(collision.gameObject);*/
            }
        }
            
        
    }
    bool newPassenger(GameObject passenger)
    {
        if (passengers.Count >= maxPassengers)
            return false;
        float x;
        float y;
        if (passengers.Count >= maxPassengers/2)
        {
            y = starty + incrementy;
            x = startx - (incrementx*3);
        }
        else
        {
            y = starty;
            x = startx;
        }
        passengers.Add(Object.Instantiate(passenger, transform.position + new Vector3(x + (incrementx * passengers.Count), y, -1), transform.rotation, transform));
        return true;
    }
}
