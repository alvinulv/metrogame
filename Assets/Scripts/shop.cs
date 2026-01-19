using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shop : MonoBehaviour
{
    EventSystem eventSystem;
    int upgradePrice1 = 0;
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
        eventSystem.SetSelectedGameObject(null);
    }
}
