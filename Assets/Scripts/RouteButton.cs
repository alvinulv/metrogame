using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteButton : MonoBehaviour
{
    public int route;
    Routelogic routelogic;
    GameObject pointer;
    // Start is called before the first frame update
    void Start()
    {
        pointer = GameObject.Find("Pointer");
        routelogic = GameObject.Find("RouteLogic").GetComponent<Routelogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchToRoute()
    {
        if (routelogic.currentRoute != route)
        {
            routelogic.currentRoute = route;
            pointer.transform.position = new Vector2(pointer.transform.position.x, transform.position.y);
        }
        else
        {
            routelogic.currentRoute = -1;
            pointer.transform.position = new Vector2(pointer.transform.position.x, -5000);
        }
    }
}
