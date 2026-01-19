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
            if (stationsToSpawn.Count <= 0)
            {
                for (int i = 0; i < stationPrefabs.Length; i++)
                {
                    stationsToSpawn.Add(stationPrefabs[i]);
                    stationsToSpawn.Add(stationPrefabs[i]);
                }
            }
            int rand = Random.Range(0, stationsToSpawn.Count);
            stationList.Add(Object.Instantiate(stationsToSpawn[rand],FindSpawn(), Quaternion.identity));
            stationsToSpawn.RemoveAt(rand);
            if (stationList.Count >= maxStations)
            {
                Destroy(gameObject);
            }
        }
        
    }
    Vector3 FindSpawn()
    {
        Vector3 temp = new Vector3(Random.Range(-xbound, xbound), Random.Range(-ybound, ybound), 0);
        for (int i = 0;i < stationList.Count;i++)
        {
            if (stationList[i].transform.position == temp)
                return FindSpawn();
        }
        return temp;
    }

}
