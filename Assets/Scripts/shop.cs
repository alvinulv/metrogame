using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class shop : MonoBehaviour
{
    public GameObject priceDisplay1;
    public GameObject priceDisplay2;
    public GameObject priceDisplay3;
    public GameObject priceDisplay4;
    public GameObject priceDisplay5;
    public GameObject moneyDisplay;
    public static int money;
    EventSystem eventSystem;
    int upgradePrice1 = 50;
    int upgradePrice2 = 25;
    int upgradePrice3 = 10;
    int upgradePrice4 = 10;
    int upgradePrice5 = 50;
    int routes = 1;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = FindFirstObjectByType<EventSystem>();
        priceDisplay1.GetComponent<TMP_Text>().text = upgradePrice1.ToString();
        priceDisplay2.GetComponent<TMP_Text>().text = upgradePrice2.ToString();
        priceDisplay3.GetComponent<TMP_Text>().text = upgradePrice3.ToString();
        priceDisplay4.GetComponent<TMP_Text>().text = upgradePrice4.ToString();
        priceDisplay5.GetComponent<TMP_Text>().text = upgradePrice5.ToString();
        moneyDisplay.GetComponent<TMP_Text>().text = "money: " + money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        moneyDisplay.GetComponent<TMP_Text>().text = "money: " + money.ToString();
    }
    public void MoreRoutes()
    {

        if (money >= upgradePrice1 && routes < 4)
        {
            routes++;
            money -= upgradePrice1;
            Routelogic.AddRoute(null);
            if (routes < 4)
            {
                float temp = (float)(upgradePrice1 * 2);
                upgradePrice1 = (int)temp;
                priceDisplay1.GetComponent<TMP_Text>().text = upgradePrice1.ToString();
            }
            else if (routes == 4)
            {
                priceDisplay1.GetComponent<TMP_Text>().text = "max";
            }
        }
        eventSystem.SetSelectedGameObject(null);
    }
    public void MoreTrains()
    {
        if (money >= upgradePrice2)
        {
            money -= upgradePrice2;
            float temp = (float)(upgradePrice2 * 1.2f);
            upgradePrice2 = (int)temp;
            Debug.Log(upgradePrice2); 
            priceDisplay2.GetComponent<TMP_Text>().text = upgradePrice2.ToString();
        }
        eventSystem.SetSelectedGameObject(null);
    }
    public void FasterTrains()
    {
        if (money >= upgradePrice3)
        {
            money -= upgradePrice3;
            Train.speed = Train.speed * 1.1f;
            float temp = (float)(upgradePrice3 * 1.2f);
            upgradePrice3 = (int)temp;
            Debug.Log(upgradePrice3);
            priceDisplay3.GetComponent<TMP_Text>().text = upgradePrice3.ToString();
        }
        eventSystem.SetSelectedGameObject(null);
    }
    public void BiggerTrains()
    {
        if (money >= upgradePrice4)
        {
            money -= upgradePrice4;
            float temp = (float)(upgradePrice4 * 1.2f);
            upgradePrice4 = (int)temp;
            Debug.Log(upgradePrice4);
            priceDisplay4.GetComponent<TMP_Text>().text = upgradePrice4.ToString();
        }
        eventSystem.SetSelectedGameObject(null);
    }
    public void Adverticement()
    {
        if (money >= upgradePrice5 && Stations.NextPersonCanSpawnMax > 2)
        {
            money -= upgradePrice5;
            if (Stations.NextPersonCanSpawnMin > 1)
            {
                Stations.NextPersonCanSpawnMin--;
                Stations.NextPersonCanSpawnMax--;
                float temp = (float)(upgradePrice5 * 1.4f);
                upgradePrice5 = (int)temp;
                Debug.Log(upgradePrice5);
                priceDisplay5.GetComponent<TMP_Text>().text = upgradePrice5.ToString();
            }
            else if (Stations.NextPersonCanSpawnMax > 2)
            {
                Stations.NextPersonCanSpawnMax--;
                if (Stations.NextPersonCanSpawnMax == 2)
                {
                    priceDisplay5.GetComponent<TMP_Text>().text = "max";
                }
                else
                {
                    float temp = (float)(upgradePrice5 * 1.2f);
                    upgradePrice5 = (int)temp;
                    priceDisplay5.GetComponent<TMP_Text>().text = upgradePrice5.ToString();
                }
            }
        }
        eventSystem.SetSelectedGameObject(null);
    }
}
