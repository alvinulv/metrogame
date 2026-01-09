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
    [SerializeField] GameObject nextStop;
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
        if (stopped <= 0)
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
        else stopped--;
    }
        
    void ReachedStop(string type)
    {
        stopped = 100;
        station = nextStop.GetComponent<Stations>();
        station.TrainIsHere = true;
        //removing passengers
        for (int i = 0; i < passengers.Length;i++)
        {
            if (passengers[i] != null)
            {
                if (passengers[i].CompareTag(type))
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
            nextStop = collision.gameObject;
            ReachedStop(collision.gameObject.tag);
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
