using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stations : MonoBehaviour
{
    public string[] people = { "null", "null", "null", "null", "null" };
    string[] possiblePeople = { "Circle", "Triangle", "Square" };
    float timeSinceLastPerson = 10;
    float nextPersonCanSpawn = 15;
    public static int NextPersonCanSpawnMin = 10;
    public static int NextPersonCanSpawnMax = 20;
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
                    //Debug.Log("new " + people[i] + " appeared");
                    timeSinceLastPerson = 0;
                    nextPersonCanSpawn = Random.Range(NextPersonCanSpawnMin * 100, NextPersonCanSpawnMax * 100) / 100f;
                    transform.Find(people[i] + " (" + i + ")").GetComponent<SpriteRenderer>().enabled = true;
                    return;
                }

            }
        }
        if (correctionOfPassengersListNeeded(people))
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
        for (int i = 0; i < passengers.Length; i++)
        {
            if (passengers[i] == "null")
            {
                for (int j = 0; j < 3; j++)
                {
                    if (transform.Find(possiblePeople[j] + " (" + i + ")").GetComponent<SpriteRenderer>().enabled == true) return true;
                }
            }
            else if (transform.Find(passengers[i] + " (" + i + ")").GetComponent<SpriteRenderer>().enabled == false) return true;
        }
        return false;
    }
    public string[] listOfPassengersUpdate(string[] passengers)
    {
        for (int i = 0; i < passengers.Length - 1; i++)
        {
            if (passengers[i] == "null" && passengers[i + 1] != "null")
            {
                passengers[i] = passengers[i + 1];
                passengers[i + 1] = "null";
            }
        }
        for (int i = 0; i < passengers.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                transform.Find(possiblePeople[j] + " (" + i + ")").GetComponent<SpriteRenderer>().enabled = false;
            }
            if (passengers[i] != "null") transform.Find(passengers[i] + " (" + i + ")").GetComponent<SpriteRenderer>().enabled = true;

        }
        return passengers;
    }
    string whatPersonShouldSpawn()
    {
        int j = Random.Range(0, 3);
        if (transform.CompareTag(possiblePeople[j])) { return whatPersonShouldSpawn(); }
        return possiblePeople[j];
    }
}
