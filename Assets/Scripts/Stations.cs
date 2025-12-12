using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stations : MonoBehaviour
{
    public string[] people = { "null", "null", "null", "null", "null" };
    float timeSinceLastPerson = 10;
    int nextPeronCanSpawn = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (people[4] == "null" && timeSinceLastPerson > 15)
        {
            Debug.Log("new person appeared");
            timeSinceLastPerson = 0;
            nextPeronCanSpawn = Random.Range(10, 20);
        }
        timeSinceLastPerson += Time.deltaTime * 1;
    }
}
