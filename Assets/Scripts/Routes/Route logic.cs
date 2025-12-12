using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Routelogic : MonoBehaviour
{
    LineRenderer lR;
    public List<GameObject> routeWaypoints = new List<GameObject>();
    [SerializeField] GameObject baseWaypoint;
    [SerializeField] bool loop;
    [Header("Debug")]
    public bool updateRoute = true;
    public bool removeFirst;
    public bool removeLast;
    void Start()
    {
        lR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateRoute)
        {
            updateRoute = false;
            UpdateRoute();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 p = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            AddRouteWaypoint(new Vector3(pos.x,pos.y,0));
            UpdateRoute();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 p = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            AddRouteWaypoint(new Vector3(pos.x, pos.y, 0), 0);
            UpdateRoute();
        }
        if (removeFirst)
        {
            RemovePoint(routeWaypoints.Count - 1);
            removeFirst = false;
        }
        if (removeLast)
        {
            RemovePoint(0);
            removeLast = false;
        }
    }
    void AddRouteWaypoint(Vector3 pos)
    {
        routeWaypoints.Add(Instantiate(baseWaypoint, pos, transform.rotation, transform));
    }
    void AddRouteWaypoint(Vector3 pos, int index){
        routeWaypoints.Insert(index, Instantiate(baseWaypoint, pos, transform.rotation, transform));
    }
    void DestroyWaypoint()
    {

    }
    void UpdateRoute(){
        lR.positionCount = routeWaypoints.Count;
        if (routeWaypoints.Count < 2)
            return;
        for(int i = 0; i < lR.positionCount; i++)
        {
            lR.SetPosition(i, routeWaypoints[i].transform.position);
        }
        if (loop)
        {
            lR.positionCount++;
            lR.SetPosition(lR.positionCount - 1, lR.GetPosition(0));
            
        }
    }
    void RemovePoint(int index)
    {

        routeWaypoints.RemoveAt(index);
        UpdateRoute();
    }
}
