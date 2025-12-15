using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stations : MonoBehaviour
{
    public string[] people = { "null", "null", "null", "null", "null" };
    float timeSinceLastPerson = 10;
    int nextPersonCanSpawn = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastPerson > nextPersonCanSpawn)
        {
            for (int i = 0; i < people.Length; i++)
            {
                if (people[i] == "null")
                {
                    people[i] = whatPersonShouldSpawn();
                    Debug.Log("new " + people[i] + " appeared");
                    timeSinceLastPerson = 0;
                    nextPersonCanSpawn = Random.Range(10, 20);
                    transform.Find(people[i] + " (" + i + ")").GetComponent<SpriteRenderer>().enabled = true;
                    return;
                }

            }
        }
        
        timeSinceLastPerson += Time.deltaTime * 1;
    }
    public string[] listOfPassengersUpdate(string[] passengers)
    {
        for (int i = 0; i < passengers.Length-1; i++)
        {
            passengers[i] = passengers[i+1];
        }
        return passengers;
    }
    string whatPersonShouldSpawn()
    {
        int j = Random.Range(1, 4);
        switch (j)
        {
            case 1: return "Circle";
            case 2: return "Triangle";
            case 3: return "Square";
            default: return "null";
        }
    }
}
