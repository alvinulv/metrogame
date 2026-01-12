using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Train : MonoBehaviour
{
    //Object.Instantiate(train).GetComponent<Train>().Route =
    [Header("Movement")]
    public GameObject Route;
    Routelogic Routelogic;
    Vector3 nextWaypoint;
    int index;
    [SerializeField] float speed = 0.03f;
    [SerializeField] float mindist = 0.1f;
    [Header("Passenger slots")]
    [SerializeField] GameObject[] passengers;
    //[SerializeField] bool[] emptySlots;
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
    GameObject lastStop;
    Stations station;
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

            /*if ((nextStop.transform.position - transform.position).magnitude < mindist)
                ReachedStop(nextStop);*/
        }
        else stopped--;
    }
        
    void ReachedStop(GameObject stop)
    {
        if (stop != lastStop)
        stopped = 100;
        station = stop.GetComponent<Stations>();
        station.TrainIsHere = true;
        //removing passengers
        for (int i = 0; i < passengers.Length;i++)
        {
            if (passengers[i] != null)
            {
                if (passengers[i].CompareTag(stop.tag))
                {
                    Destroy(passengers[i]);
                    passengers[i].transform.position = transform.position + new Vector3(startx + (incrementx * (i)), starty, -1);
                }
            }

        }
        //adding passengers
        for (int i = 0;i < station.people.Length;i++)
        {
            for (int j = 0;j <passengers.Length;j++)
                if (passengers[j] == null)
            switch (station.people[i])
            {
                case "null": break;
                case "Square": newPassenger(squarePassenger,i,j);break;
                case "Circle": newPassenger(circlePassenger,i,j); break;
                case "Triangle": newPassenger(trianglePassenger,i,j); break;
                default: break;
            }
            station.people = station.listOfPassengersUpdate(station.people);
        }
        station.TrainIsHere=false;
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
    void newPassenger(GameObject passenger, int stationSlot, int carSlot)
    {
        //always check if (emptySlots.Count > 0)
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
        //emptySlots[carSlot] = false;
        station.people[stationSlot] = "null";

        
    }
}
