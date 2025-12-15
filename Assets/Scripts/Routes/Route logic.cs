using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Routelogic : MonoBehaviour
{
    LineRenderer lR;
    public List<GameObject> rWp = new List<GameObject>();
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
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 p = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            AddRouteWaypoint(new Vector3(pos.x,pos.y,0));
            UpdateRoute();
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 p = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            if (rWp[0] != null)
            {
                rWp.Insert(0, null);
                UpdateRoute();
            }
            lR.SetPosition(0, pos);
            UpdateRoute();
        }
        if (Input.GetMouseButtonUp(1))
        {
            Vector3 p = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            AddRouteWaypoint(new Vector3(pos.x, pos.y, 0), 0);
            UpdateRoute();
        }
        if (removeFirst)
        {
            RemovePoint(rWp.Count - 1);
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
        if (rWp[rWp.Count - 1] == null)
        {
            rWp[rWp.Count - 1] = Instantiate(baseWaypoint, pos, transform.rotation, transform);
        }
        else
        {
            rWp.Add(Instantiate(baseWaypoint, pos, transform.rotation, transform));
        }
    }
    void AddRouteWaypoint(Vector3 pos, int index){
        if (rWp[index] == null)
        {
            rWp[index] = Instantiate(baseWaypoint, pos, transform.rotation, transform);
        }
        else
        {
            rWp.Insert(index, Instantiate(baseWaypoint, pos, transform.rotation, transform));
        }
    }
    void DestroyWaypoint()
    {

    }
    void UpdateRoute(){
        lR.positionCount = rWp.Count;
        if (rWp.Count < 2)
            return;
        for(int i = 0; i < lR.positionCount; i++)
        {
            lR.SetPosition(i, rWp[i].transform.position);
        }
        if (loop)
        {
            lR.positionCount++;
            lR.SetPosition(lR.positionCount - 1, lR.GetPosition(0));
        }
    }
    void RemovePoint(int index)
    {

        rWp.RemoveAt(index);
        UpdateRoute();
    }
}
