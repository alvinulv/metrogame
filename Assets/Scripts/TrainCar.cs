using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class TrainCar : MonoBehaviour
{
    GameObject lastStop;
    [Header("Passenger slots")]
    [SerializeField] GameObject[] passengers;
    [SerializeField] int rowSize = 6;
    [Header("Passenger distances")]
    [SerializeField] float startx = -0.35f;
    [SerializeField] float incrementx = 0.35f;
    [SerializeField] float starty = 0.2f;
    [SerializeField] float incrementy = -0.4f;
    [Header("Prefabs")]
    [SerializeField] GameObject squarePassenger;
    [SerializeField] GameObject circlePassenger;
    [SerializeField] GameObject trianglePassenger;
    public Train train;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReachedStop(GameObject stop)
    {
        if (stop != lastStop)
        {
            train.stopped = train.stopfortime;
            Debug.Log(stop.name);
        }
        Stations station = stop.GetComponent<Stations>();
        //removing passengers
        for (int i = 0; i < passengers.Length; i++)
        {
            if (passengers[i] != null)
            {
                if (passengers[i].CompareTag(stop.tag))
                {
                    shop.money++;
                    Destroy(passengers[i]);
                    passengers[i] = null;
                }
            }
        }
        //adding passengers
        for (int i = 0; i < station.people.Length; i++)
        {
            for (int j = 0; j < passengers.Length; j++)
            {
                if (passengers[j] == null)
                {
                    /*if (station.people[i] != null)
                    newPassenger(PassengerType(station.people[i]), i, j, station);*/
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
    void newPassenger(GameObject passenger, int stationSlot, int carSlot, Stations station)
    {
        float x;
        float y;
        if (carSlot >= rowSize)
        {
            y = starty + incrementy;
            x = startx - (incrementx * rowSize);
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
    GameObject PassengerType(string p)
    {
        switch (p)
        {
            case "Square":return squarePassenger;
            case "Circle":return circlePassenger;
            case "Triangle":return trianglePassenger;
            default: return null;
        }
    }
}
