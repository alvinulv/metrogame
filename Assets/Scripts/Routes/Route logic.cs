using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Routelogic : MonoBehaviour
{
    public static Routelogic Routelogic_;
    [SerializeField] float clickerRadius = 0.5f;
    [SerializeField] LayerMask stationLayer;
    public RaycastHit2D hit;
    [SerializeField] public List<Route> routes = new List<Route>();
    public int currentRoute = 0;
    public int currentIndex = -1;
    [NonSerialized]public bool isLoop;
    [Header("Debug")]
    public bool addRoute;
    bool clicking;
    [SerializeField] Material baseRouteColour;
    private void Awake()
    {
        Routelogic_ = this;
    }
    void Start()
    {
        AddRoute(baseRouteColour);
    }
    void Update()
    {
        if (addRoute)
        {
            addRoute = false;
            AddRoute(null);
        }
        Vector3 p = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint(p);
        hit = Physics2D.CircleCast(new Vector2(pos.x, pos.y), clickerRadius, Vector2.right, 0f, stationLayer);

        RightClick(pos);
        LeftClick(pos);

    }

    private void RightClick(Vector3 pos)
    {
        if (Input.GetMouseButtonDown(0) && hit && !clicking)
        {
            if (routes[currentRoute].route.Contains(RoundedVector(pos)))//Get index
            {
                currentIndex = routes[currentRoute].route.IndexOf(RoundedVector(pos));
            }
            else
            {
                currentIndex = routes[currentRoute].route.Count - 1;
            }

            if (hit.collider != null && routes[currentRoute].route.Count > 0 && (hit.transform.position - routes[currentRoute].route[currentIndex]).magnitude < clickerRadius)
            {
                clicking = true;
                AddRouteWaypoint(hit.transform.position, currentIndex + 1, true);
                routes[currentRoute].lR.SetPositions(routes[currentRoute].route.ToArray());
                currentIndex++;
            }
            else if (hit.collider != null && routes[currentRoute].route.Count == 0)//First waypoint
            {
                clicking = true;
                AddRouteWaypoint(hit.transform.position);
                currentIndex = 1;
                AddRouteWaypoint(hit.transform.position, currentIndex, true);
                routes[currentRoute].lR.SetPositions(routes[currentRoute].route.ToArray());
            }
           
        }

        //--------------------

        if (clicking && Input.GetMouseButton(0))
        {
            if (routes[currentRoute].route.Count > 0) ChangePos(currentIndex, RoundedVector(pos));
            int _temp = 0;
            foreach (Vector3 wP in routes[currentRoute].route)
            {
                if (wP == routes[currentRoute].route[currentIndex])
                    _temp++;
            }
            if (_temp > 1)
            {
                if (currentIndex != 0) 
                {
                    ChangePos(currentIndex, routes[currentRoute].route[currentIndex - 1]); 
                }else
                {
                    ChangePos(currentIndex, routes[currentRoute].route[currentIndex + 1]);
                }

            }
        }

        //--------------------

        if (Input.GetMouseButtonUp(0))
        {
            clicking = false;
            Vector3 _temp = routes[currentRoute].route[currentIndex];
            routes[currentRoute].route.RemoveAt(currentIndex);
            if (routes[currentRoute].route.Count > 0 && !routes[currentRoute].route.Contains(_temp))
            {
                routes[currentRoute].route.Insert(currentIndex,_temp);
            }
            if (hit.collider == null)
            {
                routes[currentRoute].route.Clear();
            }
            routes[currentRoute].lR.SetPositions(routes[currentRoute].route.ToArray());
            if (routes[currentRoute].route.Count == 0)
            {
                routes[currentRoute].lR.positionCount = 0;
            }
        }
    }

    void LeftClick(Vector3 pos)
    {
        if (Input.GetMouseButtonDown(1) && hit && !clicking) //leftclick
        {
            clicking = true;
            if (routes[currentRoute].route.Contains(RoundedVector(pos)))
            {
                currentIndex = routes[currentRoute].route.IndexOf(RoundedVector(pos));
            }
        }
        if (clicking && Input.GetMouseButton(1))//leftclick hold
        {
            if (routes[currentRoute].route.Count > 0) ChangePos(currentIndex, RoundedVector(pos));
            int _temp = 0;
            foreach (Vector3 wP in routes[currentRoute].route)
            {
                if (wP == routes[currentRoute].route[currentIndex])
                    _temp++;
            }
            if (_temp > 1) ChangePos(currentIndex, routes[currentRoute].route[currentIndex - 1]);
        }
        if (Input.GetMouseButtonUp(1))
        {
            clicking = false;
            Vector3 _temp = routes[currentRoute].route[currentIndex];
            routes[currentRoute].route.RemoveAt(currentIndex);
            if (routes[currentRoute].route.Count > 0 && !routes[currentRoute].route.Contains(_temp) && hit.collider != null)
            {
                routes[currentRoute].route.Insert(currentIndex, _temp);
            }
            routes[currentRoute].lR.SetPositions(routes[currentRoute].route.ToArray());
            if (routes[currentRoute].route.Count == 0)//debug for if rWp is empty
            {
                routes[currentRoute].lR.positionCount = 0;
            }
        }
    }

    void AddRouteWaypoint(Vector3 _pos)
    {
        if (routes[currentRoute].route.Contains(_pos))
        {
            return;
        }
        routes[currentRoute].route.Add(_pos);
        routes[currentRoute].lR.positionCount = routes[currentRoute].route.Count;
        routes[currentRoute].lR.SetPositions(routes[currentRoute].route.ToArray());
    }

    void AddRouteWaypoint(Vector3 _pos, int _index, bool _override)
    {
        routes[currentRoute].route.Insert(_index, _pos);
        routes[currentRoute].lR.positionCount = routes[currentRoute].route.Count;
        routes[currentRoute].lR.SetPositions(routes[currentRoute].route.ToArray());
    }
    Vector3 RoundedVector(Vector3 _pos)
    {
        return new Vector3(Mathf.RoundToInt(_pos.x), Mathf.RoundToInt(_pos.y), 0);
    }
    void ChangePos(int _index, Vector3 _pos)
    {
        routes[currentRoute].route[_index] = _pos;
        routes[currentRoute].lR.SetPositions(routes[currentRoute].route.ToArray());
    }
    public static void AddRoute(Material _mat)
    {
        GameObject _tempObj = Instantiate(new GameObject(),Routelogic_.transform);
        LineRenderer _temp = _tempObj.AddComponent<LineRenderer>();
        Routelogic_.routes.Add(new Route(_temp, _mat));
        GameObject.Find("Canvas").GetComponent<GameManagement>().AddRouteButton(_mat);
    }
}