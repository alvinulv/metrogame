using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] Transform nextStop;
    public float speed = 0.03f;
    public float mindist = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (nextStop.position - transform.position).normalized * speed;
        if ((nextStop.position - transform.position).magnitude < mindist)
            switchDirection();
    }
    void switchDirection()
    {
        Debug.Log("Hi!");
    }
}
