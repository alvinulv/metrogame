using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shop : MonoBehaviour
{
    public GameObject priceDisplay1;
    public GameObject priceDisplay2;
    public GameObject priceDisplay3;
    public GameObject priceDisplay4;
    public GameObject priceDisplay5;
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
        //priceDisplay1.GetComponent<Text>().text = upgradePrice1.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoreRoutes()
    {
        if (money >= upgradePrice1)
        {
            money =- upgradePrice1;
            Routelogic.AddRoute();
            upgradePrice1 = (int)(upgradePrice1 * 1.2f);
            priceDisplay1.GetComponent<Text>().SetText(upgradePrice1.ToString());
        }
        eventSystem.SetSelectedGameObject(null);
    }
    public void MoreTrains()
    {
        if (money >= upgradePrice2)
        {
            money =- upgradePrice2;
            upgradePrice2 = (int)(upgradePrice2 * 1.2f);
           // priceDisplay2.GetComponent<Text>().text = upgradePrice2.ToString();
        }
        eventSystem.SetSelectedGameObject(null);
    }
    public void FasterTrains()
    {
        if (money >= upgradePrice3)
        {
            money =- upgradePrice3;
            upgradePrice3 = (int)(upgradePrice3 * 1.2f);
           // priceDisplay3.GetComponent<Text>().text = upgradePrice3.ToString();
        }
        eventSystem.SetSelectedGameObject(null);
    }
    public void BiggerTrains()
    {
        if (money >= upgradePrice4)
        {
            money =- upgradePrice4;
            upgradePrice4 = (int)(upgradePrice4 * 1.2f);
            //priceDisplay4.GetComponent<Text>().text = upgradePrice4.ToString();
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
            //priceDisplay5.GetComponent<Text>().text = upgradePrice5.ToString();
        }
        eventSystem.SetSelectedGameObject(null);
    }
}
