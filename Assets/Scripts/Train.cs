using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Train : MonoBehaviour
{
    //Object.Instantiate(train).GetComponent<Train>().Route =
    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject carPrefab2;
    [Header("Movement")]
    public GameObject Route;
    Routelogic Routelogic;
    Vector3 nextWaypoint;
    int index;
    public static float speed = 0.01f;
    [SerializeField] float mindist = 0.1f;
    public float stopfortime = 0.5f;
    public List<TrainCar> cars = new List<TrainCar>();
    bool reverse;
    [HideInInspector] public float stopped;
    
    //List<int> removed;
    
    // Start is called before the first frame update
    void Start()
    {
        Routelogic = Route.GetComponent<Routelogic>();
        
        foreach (TrainCar car in cars)
            car.train = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            NewCar();
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
                    if (index > Routelogic.routes[Routelogic.currentRoute].route.Count)
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
                foreach (GameObject station in StationSpawner.stationList)
                {
                    if (nextWaypoint == station.transform.position)
                        foreach (TrainCar car in cars)
                        {
                            car.ReachedStop(station);
                        }
                }
                nextWaypoint = Routelogic.routes[Routelogic.currentRoute].route[index];
            }
        }
        else stopped -= Time.deltaTime;
    }
    void NewCar()
    {
        switch (cars.Count)
        {
            case 1:
                cars[0].transform.position += new Vector3(0,0.16f);
                cars.Add(Object.Instantiate(carPrefab,this.transform).GetComponent<TrainCar>());
                cars[1].transform.position += new Vector3(0, -0.16f);
                cars[1].train = this;   
                break;
            case 2:
                cars.Add(Object.Instantiate(carPrefab2, this.transform).GetComponent<TrainCar>());
                cars[2].train = this;
                cars[2].transform.position += new Vector3(0.48f, 0);
                break;
            case 3:
                cars.Add(Object.Instantiate(carPrefab2, this.transform).GetComponent<TrainCar>());
                cars[3].train = this;
                cars[3].transform.position += new Vector3(0.80f, 0);
                break;
            case 4:
                cars.Add(Object.Instantiate(carPrefab2, this.transform).GetComponent<TrainCar>());
                cars[4].train = this;
                cars[4].transform.position += new Vector3(-0.48f, 0);
                break;
            default:break;
        }
        
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
        /*if (collision.gameObject.layer ==6)
        {
            foreach (TrainCar car in cars)
            {
                car.ReachedStop(collision.gameObject);
            }
            
        }*/
    }
}
