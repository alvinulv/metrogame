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
        if (money > upgradePrice1)
        {
            money =- upgradePrice1;
            Routelogic.AddRoute();
            upgradePrice1 = (int)(upgradePrice1 * 1.2f);
        }
        eventSystem.SetSelectedGameObject(null);
    }
    public void MoreTrains()
    {
        if (money > upgradePrice2)
        {
            money =- upgradePrice2;
            upgradePrice2 = (int)(upgradePrice2 * 1.2f);
        }
        eventSystem.SetSelectedGameObject(null);
    }
    public void FasterTrains()
    {
        if (money > upgradePrice3)
        {
            money =- upgradePrice3;
            upgradePrice3 = (int)(upgradePrice3 * 1.2f);
        }
        eventSystem.SetSelectedGameObject(null);
    }
    public void BiggerTrains()
    {
        if (money > upgradePrice4)
        {
            money =- upgradePrice4;
            upgradePrice4 = (int)(upgradePrice4 * 1.2f);
        }
        eventSystem.SetSelectedGameObject(null);
    }
    public void Adverticement()
    {
        if (money >= upgradePrice5)
        {
            money =- upgradePrice5;
            if (Stations.NextPersonCanSpawnMin! <= 1)
            {
                Stations.NextPersonCanSpawnMin--;
                Stations.NextPersonCanSpawnMax--;
            }
            else if (Stations.NextPersonCanSpawnMax! <= 2)
            {
                Stations.NextPersonCanSpawnMax--;
            }
            upgradePrice5 = (int)(upgradePrice5 * 1.2f);
        }
        eventSystem.SetSelectedGameObject(null);
    }
}
