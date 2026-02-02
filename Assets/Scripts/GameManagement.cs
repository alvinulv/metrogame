using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    [SerializeField] GameObject shop;
    [SerializeField] GameObject routebutton;
    bool open = false;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenShop()
    {
        open = !open;
        shop.SetActive(open);
        if (open)
        Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    public void AddRouteButton(Material _mat)
    {
        if (i < 4)
        {
            GameObject button = Object.Instantiate(routebutton);
            button.transform.parent = this.transform;
            button.GetComponent<RectTransform>().localPosition = new Vector2(-144, 90 - (i * 21));
            button.GetComponent<RouteButton>().route = i;
            button.GetComponent<Image>().material = _mat;
            i++;
            button.GetComponentInChildren<TMP_Text>().text = "Route " + i;
        }
        
    }
}
