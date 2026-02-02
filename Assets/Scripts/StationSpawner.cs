using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] stationPrefabs;
    public static List<GameObject> stationList = new List<GameObject>();
    List<GameObject> stationsToSpawn = new List<GameObject>();
    [SerializeField] int maxStations;
    [SerializeField] int xbound;
    [SerializeField] int ybound;
    [SerializeField] float frequency;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < stationPrefabs.Length; i++)
        {
            stationsToSpawn.Add(stationPrefabs[i]);
            SpawnStation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
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
            SpawnStation();
            if (stationList.Count >= maxStations)
            {
                Destroy(gameObject);
            }
        }

    }
    Vector3 FindSpawn()
    {
        Vector3 temp = new Vector3(2*Random.Range(-xbound, xbound),2*Random.Range(-ybound, ybound), 0);
        foreach (GameObject station in stationList)
        {
            if (station.transform.position == temp)
                return FindSpawn();
        }
        return temp;
    }
    void SpawnStation()
    {
        int rand = Random.Range(0, stationsToSpawn.Count);
        stationList.Add(Object.Instantiate(stationsToSpawn[rand], FindSpawn(), Quaternion.identity));
        stationsToSpawn.RemoveAt(rand);
    }
}
