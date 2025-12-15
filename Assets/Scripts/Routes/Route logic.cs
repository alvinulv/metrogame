using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Routelogic : MonoBehaviour
{
    LineRenderer lR;
    public List<Vector3> rWp = new List<Vector3>();
    [SerializeField] GameObject baseWaypoint;
    public bool isLoop;
    [Header("Debug")]
    public bool removeFirst;
    public bool removeLast;
    void Start()
    {
        lR = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 p = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            AddRouteWaypoint(RoundedVector(pos));
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 p = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            ChangePos(rWp.Count - 1, RoundedVector(pos));
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 p = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            AddRouteWaypoint(RoundedVector(pos), 0);
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 p = Input.mousePosition;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            ChangePos(0, RoundedVector(pos));
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

        if (rWp[0] == rWp[rWp.Count - 1])
        {
            isLoop = true;
        }
        else
        {
            isLoop = false;
        }
    }
    void AddRouteWaypoint(Vector3 pos)
    {
        rWp.Add(pos);
        lR.positionCount = rWp.Count;
        lR.SetPositions(rWp.ToArray());
    }
    void AddRouteWaypoint(Vector3 pos, int index)
    {
        if (rWp.Count < 2)
        {
            AddRouteWaypoint(pos);
        }
        else
        {
            rWp.Insert(index, pos);
        }
        lR.positionCount = rWp.Count;
        lR.SetPositions(rWp.ToArray());
    }
    void RemovePoint(int index)
    {
        rWp.RemoveAt(index);
        lR.SetPositions(rWp.ToArray());
    }
    Vector3 RoundedVector(Vector3 pos)
    {
        return new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0);
    }
    void ChangePos(int index, Vector3 pos)
    {
        rWp[index] = pos;
        lR.SetPositions(rWp.ToArray());
    }
}
