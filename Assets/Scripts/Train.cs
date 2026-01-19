using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Train : MonoBehaviour
{
    //Object.Instantiate(train).GetComponent<Train>().Route =
    GameObject lastStop;
    [Header("Movement")]
    public GameObject Route;
    Routelogic Routelogic;
    Vector3 nextWaypoint;
    int index;
    public static float speed = 0.01f;
    [SerializeField] float mindist = 0.1f;
    [Header("Passenger slots")]
    [SerializeField] GameObject[] passengers;
    [SerializeField] int maxPassengers = 6;
    [Header("Passenger distances")]
    [SerializeField] float startx = -0.35f;
    [SerializeField] float incrementx = 0.35f;
    [SerializeField] float starty = 0.2f;
    [SerializeField] float incrementy = -0.4f;
    [Header("Prefabs")]
    [SerializeField] GameObject squarePassenger;
    [SerializeField] GameObject circlePassenger;
    [SerializeField] GameObject trianglePassenger;
    
    
    bool reverse;
    int stopped;
    //List<int> removed;
    
    // Start is called before the first frame update
    void Start()
    {
        Routelogic = Route.GetComponent<Routelogic>();
        /*for (int i = 0; i < maxPassengers; i++)
        {
            passengers.Add(null);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //Moving the train along the route
        if (stopped <= 0)
        {
            transform.position = transform.position + (nextWaypoint - transform.position).normalized * speed;
            if ((nextWaypoint - transform.position).magnitude < mindist)
            {
                //Runs when a waypoint is reached
                if (!reverse)
                {
                    index++;
                    if (index > Routelogic.Routes[Routelogic.currentRoute].Count)
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
                nextWaypoint = Routelogic.Routes[Routelogic.currentRoute][index];
            }

            /*if ((nextStop.transform.position - transform.position).magnitude < mindist)
                ReachedStop(nextStop);*/
        }
        else stopped--;
    }
        
    void ReachedStop(GameObject stop)
    {
        if (stop != lastStop)
        stopped = 100;
        Stations station = stop.GetComponent<Stations>();
        //removing passengers
        for (int i = 0; i < passengers.Length;i++)
        {
            if (passengers[i] != null)
            {
                if (passengers[i].CompareTag(stop.tag))
                {
                    Destroy(passengers[i]);
                    passengers[i] = null;
                }
            }

        }
        //adding passengers
        for (int i = 0;i < station.people.Length;i++)
        {
            for (int j = 0;j <passengers.Length;j++)
            {
                if (passengers[j] == null)
                {
                    switch (station.people[i])
                    {
                        case "null": break;
                        case "Square": newPassenger(squarePassenger, i, j, station); break;
                        case "Circle": newPassenger(circlePassenger, i, j, station); break;
                        case "Triangle": newPassenger(trianglePassenger, i, j, station); break;
                        default: break;
                    }
                    
                }
            }
        }
        station.people = station.listOfPassengersUpdate(station.people);
        lastStop = stop;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        {
            /*if (emptySlots.Count > 0)
                if (!passengers.Contains(collision.gameObject))
                {
                    newPassenger(collision.gameObject);
                }*/
        }
        if (collision.gameObject.layer ==6)
        {
            ReachedStop(collision.gameObject);
        }
       
    }
    void newPassenger(GameObject passenger, int stationSlot, int carSlot, Stations station)
    {
        float x;
        float y;
        if (carSlot >= maxPassengers / 2)
        {
            y = starty + incrementy;
            x = startx - (incrementx * 3);
        }
        else
        {
            y = starty;
            x = startx;
        }
        GameObject p = Object.Instantiate(passenger, transform.position + new Vector3(x + (incrementx * (carSlot)), y, -1), transform.rotation);
        p.transform.parent = transform;
        passengers[carSlot] = p;
        station.people[stationSlot] = "null";
    }
}
