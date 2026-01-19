using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Routelogic : MonoBehaviour
{
    public static Routelogic Routelogic_;
    [SerializeField] float clickerRadius = 0.5f;
    [SerializeField] LayerMask stationLayer;
    public RaycastHit2D hit;
    LineRenderer lR;
    [SerializeField] public List<List<Vector3>> Routes = new List<List<Vector3>>();
    public int currentRoute = 0;
    int currentIndex = -1;
    [NonSerialized]public bool isLoop;
    [Header("Debug")]
    bool clicking;
    private void Awake()
    {
        Routelogic_ = this;
    }
    void Start()
    {
        AddRoute();
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
            if (Routes[currentRoute].Contains(RoundedVector(pos)))//Get index
            {
                currentIndex = Routes[currentRoute].IndexOf(RoundedVector(pos));
            }
            else
            {
                currentIndex = Routes[currentRoute].Count - 1;
            }

            if (hit.collider != null && Routes[currentRoute].Count > 0 && (hit.transform.position - Routes[currentRoute][currentIndex]).magnitude < clickerRadius)
            {
                clicking = true;
                AddRouteWaypoint(hit.transform.position, currentIndex, true);
                lR.SetPositions(Routes[currentRoute].ToArray());
            } else if (hit.collider != null && Routes[currentRoute].Count == 0)//First waypoint
            {
                clicking = true;
                AddRouteWaypoint(hit.transform.position);
                AddRouteWaypoint(hit.transform.position, currentIndex,true);
                lR.SetPositions(Routes[currentRoute].ToArray());
            }
        }
        if (clicking && Input.GetMouseButton(0))
        {
            if(Routes[currentRoute].Count > 0) ChangePos(Routes[currentRoute].Count - 1, RoundedVector(pos));
            int _temp = 0;
            foreach (Vector3 wP in Routes[currentRoute])
            {
                if (wP == Routes[currentRoute][Routes[currentRoute].Count - 1])
                    _temp++;
            }
            if (_temp > 1) ChangePos(Routes[currentRoute].Count - 1, Routes[currentRoute][Routes[currentRoute].Count -2]);
        }
        if (Input.GetMouseButtonUp(0))
        {
            clicking = false;
            Vector3 _temp = Routes[currentRoute][Routes[currentRoute].Count-1];
            Routes[currentRoute].RemoveAt(Routes[currentRoute].Count - 1);
            if (Routes[currentRoute].Count > 0 && !Routes[currentRoute].Contains(_temp))
            {
                Routes[currentRoute].Add(_temp);
            }
            if (hit.collider == null)
            {
                Routes[currentRoute].Clear();
            }
            lR.SetPositions(Routes[currentRoute].ToArray());
            if(Routes[currentRoute].Count == 0)
            {
                lR.positionCount = 0;
            }
        }
        //----------------------------
        if (Input.GetMouseButtonDown(1) && hit && !clicking) //leftclick
        {
            clicking = true;
            if (Routes[currentRoute].Contains(RoundedVector(pos)))
            {
                currentIndex = Routes[currentRoute].IndexOf(RoundedVector(pos));
            }
        }
        if (clicking && Input.GetMouseButton(1))//leftclick hold
        {
            if (Routes[currentRoute].Count > 0) ChangePos(currentIndex, RoundedVector(pos));
            int _temp = 0;
            foreach (Vector3 wP in Routes[currentRoute])
            {
                if (wP == Routes[currentRoute][currentIndex])
                    _temp++;
            }
            if (_temp > 1) ChangePos(currentIndex, Routes[currentRoute][currentIndex-1]);
        }
        if (Input.GetMouseButtonUp(1))
        {
            clicking = false;
            Vector3 _temp = Routes[currentRoute][currentIndex];
            Routes[currentRoute].RemoveAt(currentIndex);
            if (Routes[currentRoute].Count > 0 && !Routes[currentRoute].Contains(_temp) && hit.collider != null)
            {
                Routes[currentRoute].Insert(currentIndex, _temp);
            }
            lR.SetPositions(Routes[currentRoute].ToArray());
            if (Routes[currentRoute].Count == 0)//debug for if rWp is empty
            {
                lR.positionCount = 0;
            }
        }
        //------------------------------
        if (Routes[currentRoute].Count > 1 && Routes[currentRoute][0] == Routes[currentRoute][Routes[currentRoute].Count - 1])
        {
            isLoop = true;
        }
        else
        {
            isLoop = false;
        }
    }
    void AddRouteWaypoint(Vector3 _pos)
    {
        if (Routes[currentRoute].Contains(_pos))
        {
            return;
        }
        Routes[currentRoute].Add(_pos);
        lR.positionCount = Routes[currentRoute].Count;
        lR.SetPositions(Routes[currentRoute].ToArray());
    }
    void AddRouteWaypoint(Vector3 _pos, int _index)
    {
        if (Routes[currentRoute].Contains(_pos))
        {
            return;
        }
        if (Routes[currentRoute].Count < 2)
        {
            AddRouteWaypoint(_pos);
        }
        else
        {
            Routes[currentRoute].Insert(_index, _pos);
        }
        lR.positionCount = Routes[currentRoute].Count;
        lR.SetPositions(Routes[currentRoute].ToArray());
    }
    void AddRouteWaypoint(Vector3 _pos, int _index, bool _override)
    {
        Routes[currentRoute].Add(_pos);
        lR.positionCount = Routes[currentRoute].Count;
        lR.SetPositions(Routes[currentRoute].ToArray());
    }
    Vector3 RoundedVector(Vector3 _pos)
    {
        return new Vector3(Mathf.RoundToInt(_pos.x), Mathf.RoundToInt(_pos.y), 0);
    }
    void ChangePos(int _index, Vector3 _pos)
    {
        Routes[currentRoute][_index] = _pos;
        lR.SetPositions(Routes[currentRoute].ToArray());
    }
    public static void AddRoute()
    {
        Routelogic_.Routes.Add(new List<Vector3>());
    }
}
