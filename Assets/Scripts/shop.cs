using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shop : MonoBehaviour
{
    public static int money;
    EventSystem eventSystem;
    int upgradePrice1 = 50;
    int upgradePrice2 = 25;
    int upgradePrice3 = 10;
    int upgradePrice4 = 10;
    int upgradePrice5 = 100;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = FindFirstObjectByType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void upgrade1()
    {
        upgradePrice1++;
        float tempUpgradePrice1 = upgradePrice1;
        upgradePrice1 = (int)(upgradePrice1 * 1.2f);
        Debug.Log(upgradePrice1);


    }
    public void MoreRoutes()
    {
        Routelogic.AddRoute();
        eventSystem.SetSelectedGameObject(null);
    }
    public void MoreTrains()
    {
        eventSystem.SetSelectedGameObject(null);
    }
    public void FasterTrains()
    {
        eventSystem.SetSelectedGameObject(null);
    }
    public void BiggerTrains()
    {
        eventSystem.SetSelectedGameObject(null);
    }
    public void Adverticement()
    {
        if (Stations.NextPersonCanSpawnMin !<= 1)
        {
            Stations.NextPersonCanSpawnMin--;
            Stations.NextPersonCanSpawnMax--;
        }
        else if (Stations.NextPersonCanSpawnMax !<= 2)
        {
            Stations.NextPersonCanSpawnMax--;
        }
        upgradePrice5 = (int)(upgradePrice5 * 1.2f);
        eventSystem.SetSelectedGameObject(null);
    }
}
