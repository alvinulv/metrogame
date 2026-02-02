using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    [SerializeField] GameObject shop;
    [SerializeField] GameObject routebutton;
    [SerializeField] GameObject trainPrefab;
    bool open = false;
    int i = 0;
    public int trainSize = 0;
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
        GameObject button = Object.Instantiate(routebutton, this.transform);
        Train t = Object.Instantiate(trainPrefab).GetComponent<Train>();
        t.route = i;
        button.GetComponent<RectTransform>().localPosition = new Vector2(-380, 232 - (i * 50));
        button.GetComponent<RouteButton>().route = i;
        if (_mat != null)
            button.GetComponent<Image>().color = _mat.color;
        i++;
        button.GetComponentInChildren<TMP_Text>().text = "Route " + i;
    }
}
