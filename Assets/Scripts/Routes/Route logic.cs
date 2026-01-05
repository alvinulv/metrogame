using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Routelogic : MonoBehaviour
{
    [SerializeField] float clickerRadius = 0.5f;
    [SerializeField] LayerMask stationLayer;
    public RaycastHit2D hit;
    LineRenderer lR;
    public List<Vector3> rWp = new List<Vector3>();
    [SerializeField] GameObject baseWaypoint;
    [NonSerialized]public bool isLoop;
    [Header("Debug")]
    public bool removeFirst;
    public bool removeLast;
    bool clicking;
    void Start()
    {
        lR = GetComponent<LineRenderer>();
    }
    void Update()
    {
        Vector3 p = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint(p);
        hit = Physics2D.CircleCast(new Vector2(pos.x, pos.y), clickerRadius, Vector2.right,0f,stationLayer);
        Debug.Log(hit.collider);
        //-------------------------------
        if (Input.GetMouseButtonDown(0) && hit && !clicking)
        {
            
            if (hit.collider != null && rWp.Count > 0 && (hit.transform.position - rWp[rWp.Count-1]).magnitude < clickerRadius)
            {
                clicking = true;
                if (rWp.Count == 0) AddRouteWaypoint(hit.transform.position);
                AddRouteWaypoint(hit.transform.position, true);
                lR.SetPositions(rWp.ToArray());
            } else if (hit.collider != null && rWp.Count == 0)
            {
                clicking = true;
                if (rWp.Count == 0) AddRouteWaypoint(hit.transform.position);
                AddRouteWaypoint(hit.transform.position, true);
                lR.SetPositions(rWp.ToArray());
            }
            
        }
        if (clicking && Input.GetMouseButton(0))
        {
            if(rWp.Count > 0) ChangePos(rWp.Count - 1, RoundedVector(pos));
        }
        if (Input.GetMouseButtonUp(0))
        {
            clicking = false;
            Vector3 _temp = rWp[rWp.Count-1];
            rWp.Remove(_temp);
            if (rWp.Count > 0 && !rWp.Contains(_temp))
            {
                rWp.Add(_temp);
            }
            if (hit.collider == null)
            {
                rWp.Clear();
            }
            lR.SetPositions(rWp.ToArray());
            if(rWp.Count == 0)
            {
                lR.positionCount = 0;
            }
        }
        //----------------------------
        if (Input.GetMouseButtonDown(1) && hit && !clicking)
        {
            if (hit.collider != null)
            {
                clicking = true;
                AddRouteWaypoint(RoundedVector(pos));
                AddRouteWaypoint(RoundedVector(pos));
            }
            lR.SetPositions(rWp.ToArray());
        }
        if (clicking && Input.GetMouseButton(1))
        {
            if (rWp.Count > 0) ChangePos(0, RoundedVector(pos));
        }
        if (Input.GetMouseButtonUp(1))
        {
            clicking = false;
            if (hit.collider == null)
                rWp.Clear();
            lR.SetPositions(rWp.ToArray());
            if (rWp.Count == 0)
            {
                lR.positionCount = 0;
            }
        }
        //------------------------------
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

        if (rWp.Count > 1 && rWp[0] == rWp[rWp.Count - 1])
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
        if (rWp.Contains(pos))
        {
            return;
        }
        rWp.Add(pos);
        lR.positionCount = rWp.Count;
        lR.SetPositions(rWp.ToArray());
    }
    void AddRouteWaypoint(Vector3 pos, int index)
    {
        if (rWp.Contains(pos))
        {
            return;
        }
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
    void AddRouteWaypoint(Vector3 pos, bool _override)
    {
        rWp.Add(pos);
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
