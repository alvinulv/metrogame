using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] GameObject Route;
    [SerializeField] Vector3 nextWaypoint;
    [SerializeField] GameObject nextStop;
    [SerializeField] List<GameObject> passengers = new List<GameObject>();
    Routelogic Routelogic;
    [SerializeField] int index;
    [SerializeField] bool loop;
    [SerializeField] float startx = -0.4f;
    [SerializeField] float incrementx = 0.3f;
    [SerializeField] float starty = 0.2f;
    [SerializeField] GameObject squarePas;
    bool reverse;
    public float speed = 0.03f;
    public float mindist = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Routelogic = Route.GetComponent<Routelogic>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (nextWaypoint - transform.position).normalized * speed;
        if ((nextWaypoint - transform.position).magnitude < mindist)
        {
            //Debug.Log("Waypoint reached");
            if (!reverse)
            {
                index++;
                if (index > Routelogic.rWp.Count)
                {
                    if (loop)
                        index = 0;
                    else
                    {
                        reverse = true;
                        index--;
                    }
                }
            }
            else
            {
                index--;
                if (index < 0)
                {
                    reverse = false;
                    index = 0;
                }
            }
            nextWaypoint = Routelogic.rWp[index];
        }
            
        if ((nextStop.transform.position - transform.position).magnitude < mindist)
            ReachedStop(nextStop.tag);
    }
    void ReachedStop(string type)
    {
        for (int i = 0; i < passengers.Count; i++)
        {
            if (passengers[i].CompareTag(type))
            {
                Destroy(passengers[i]);
                passengers.RemoveAt(i);
                i--;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Square"))
        {
            if (!passengers.Contains(collision.gameObject))
            {
                passengers.Add(Object.Instantiate(squarePas, new Vector3(startx + (incrementx * passengers.Count), starty, -1), transform.rotation, transform));
                /*collision.gameObject.transform.parent = transform;
                collision.gameObject.transform.position = transform.position + new Vector3(startx + (incrementx * passengers.Count), starty, -1);
                passengers.Add(collision.gameObject);*/
            }
        }
            
        
    }
}
