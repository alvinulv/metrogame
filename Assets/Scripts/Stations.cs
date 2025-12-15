using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stations : MonoBehaviour
{
    public string[] people = { "null", "null", "null", "null", "null" };
    float timeSinceLastPerson = 10;
    int nextPersonCanSpawn = 15;
    public bool TrainIsHere = false;
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
        if (correctionOfPassengersListNeeded(people) && !TrainIsHere)
        {
            people = listOfPassengersUpdate(people);
        }
        timeSinceLastPerson += Time.deltaTime * 1;
    }
    bool correctionOfPassengersListNeeded(string[] passengers)
    {
        bool previousSeenNull = false;
        for (int i = 0; i < passengers.Length; i++)
        {
            if (passengers[i] == "null")
            {
                previousSeenNull = true;
            }
            else if (previousSeenNull)
            {
                return true;
            }
        }
        return false;
    }
    string[] listOfPassengersUpdate(string[] passengers)
    {
        for (int i = 0; i < passengers.Length-1; i++)
        {
            if (passengers[i] == "null" && passengers[i + 1] != "null")
            {
                passengers[i] = passengers[i + 1];
                passengers[i + 1] = "null";
            }
        }
        for (int i = 0; i < passengers.Length; i++)
        {
                transform.Find("Circle (" + i + ")").GetComponent<SpriteRenderer>().enabled = false;
                transform.Find("Square (" + i + ")").GetComponent<SpriteRenderer>().enabled = false;
                transform.Find("Triangle (" + i + ")").GetComponent<SpriteRenderer>().enabled = false;
            if (passengers[i] != "null") transform.Find(passengers[i] + " (" + i + ")").GetComponent<SpriteRenderer>().enabled = true;

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
