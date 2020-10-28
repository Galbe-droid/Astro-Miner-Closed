using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAsteroidMovement : MonoBehaviour
{
    public float speed = 500;
    private Rigidbody miniAsteroidRb;
    private GameObject destination;
    private float yLimit = 6f;


    //Circulo Delimitador 
    float radius = 150f;
    Vector3 centerPosition = new Vector3(0, 6, 0);

    // Start is called before the first frame update
    void Start()
    {
        miniAsteroidRb = GetComponent<Rigidbody>();
        destination = GameObject.Find("Magnet");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LimitZone();
        LateDestination();
    }

    void LateDestination()
    {
        Vector3 lookDirection = (destination.transform.position - transform.position).normalized;
        miniAsteroidRb.AddForce(lookDirection * speed, ForceMode.Acceleration);
    }
       
    void LimitZone()
    {
        float distance = Vector3.Distance(miniAsteroidRb.transform.position, centerPosition);

        if (distance > radius)
        {
            Vector3 radiusLimit = miniAsteroidRb.transform.position - centerPosition;
            radiusLimit *= radius / distance;
            miniAsteroidRb.transform.position = centerPosition + radiusLimit;
        }

        if(transform.position.y == yLimit)
        {
            transform.Translate(transform.position.x, yLimit, transform.position.z);
        }
    }

    public void Absorption()
    {
        if(gameObject.tag == "Magnet")
        {
            Destroy(gameObject);
        }
    }
}
