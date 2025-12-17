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
    [SerializeField] List<GameObject> passengers = new List<GameObject>();
    [SerializeField] List<int> emptySlots;
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
    
    // Start is called before the first frame update
    void Start()
    {
        Routelogic = Route.GetComponent<Routelogic>();
        for (int i = 0; i < maxPassengers; i++)
        {
            emptySlots.Add(i);
        }
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
        station.TrainIsHere = true;
        //removing passengers
        for (int i = 0; i < passengers.Count; i++)
        {
            if (passengers[i].CompareTag(type))
            {
                Destroy(passengers[i]);
                emptySlots.Add(i);
            }
        }
        //adding passengers
        for (int i = 0;i < station.people.Length;i++)
        {
            if (emptySlots.Count > 0)
            switch (station.people[i])
            {
                case "Square": newPassenger(squarePassenger); break;
                case "Circle": newPassenger(circlePassenger); break;
                case "Triangle": newPassenger(trianglePassenger); break;
                case "null":break;
                default: break;
            }
                
        }
        station.TrainIsHere=false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        {
            if (emptySlots.Count > 0)
                if (!passengers.Contains(collision.gameObject))
                {
                    newPassenger(collision.gameObject);
                }
        }
    }
    void newPassenger(GameObject passenger)
    {
        //always check if (emptySlots.Count > 0)
        float x;
        float y;
        if (emptySlots[0] >= maxPassengers / 2)
        {
            y = starty + incrementy;
            x = startx - (incrementx * 3);
        }
        else
        {
            y = starty;
            x = startx;
        }
        GameObject p = Object.Instantiate(passenger, transform.position + new Vector3(x + (incrementx * (emptySlots[0])), y, -1), transform.rotation);
        p.transform.parent = transform;
        passengers.Insert(emptySlots[0], p);
        emptySlots.RemoveAt(0);
    }
}
