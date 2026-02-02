using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteButton : MonoBehaviour
{
    public int route;
    Routelogic routelogic;
    // Start is called before the first frame update
    void Start()
    {
        routelogic = GameObject.Find("RouteLogic").GetComponent<Routelogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchToRoute()
    {
        routelogic.currentRoute = route;
    }
}
