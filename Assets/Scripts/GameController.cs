using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] stationPrefabs;
    List<GameObject> stationList = new List<GameObject>();
    List<GameObject> stationsToSpawn = new List<GameObject>();
    [SerializeField] int maxStations;
    [SerializeField] int xbound;
    [SerializeField] int ybound;
    [SerializeField] float frequency;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        stationspawnreset();
    }

    // Update is called once per frame
    void Update()
    {
        //VERY W.I.P.
        //still need to make them not spawn on top of eachother
        //skriver også til mig selv at huske at skrive rapport
        time+=Time.deltaTime;
        if (time > frequency)
        {

            time = 0;
            if (stationsToSpawn.Count > 0)
            {
                stationList.Add(Object.Instantiate(stationPrefabs[Random.Range(0, stationPrefabs.Length)], new Vector3(Random.Range(-xbound, xbound), Random.Range(-ybound, ybound), 0), Quaternion.identity));
                if (stationList.Count >= maxStations)
                {
                    Destroy(gameObject);
                }
            }
            
        }
        
    }
    void stationspawnreset()
    {
        for (int i = 0; i < stationPrefabs.Length; i++)
        {
            stationsToSpawn.Add(stationPrefabs[i]);
            stationsToSpawn.Add(stationPrefabs[i]);
        }
    }
}
