using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float speed = 0.5f;
    private Rigidbody asteroidRigid;
    private GameObject destination;
    private float yLimit = 6;

    

    //Circulo Delimitador 
    float radius = 250f;
    Vector3 centerPosition = new Vector3(0, 6, 0);

    //animação
    private float randomTorque = 3600;
    

    // Start is called before the first frame update
    void Start()
    {
        asteroidRigid = GetComponent<Rigidbody>();
        destination = GameObject.Find("Magnet");
       
        asteroidRigid.AddTorque(Random.Range(-randomTorque, randomTorque), Random.Range(-randomTorque, randomTorque), Random.Range(-randomTorque, randomTorque), ForceMode.Acceleration);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 lookAt = (destination.transform.position - transform.position).normalized;

        asteroidRigid.AddForce(lookAt * speed, ForceMode.Acceleration);

        LimitCircle();
    }

    void LimitCircle()
    {
        float distance = Vector3.Distance(asteroidRigid.transform.position, centerPosition);

        if (distance > radius)
        {
            Vector3 radiusLimit = asteroidRigid.transform.position - centerPosition;
            radiusLimit *= radius / distance;
            asteroidRigid.transform.position = centerPosition + radiusLimit;
        }

        if (transform.position.y == yLimit)
        {
            transform.Translate(transform.position.x, yLimit, transform.position.z);
        }
    }

    
}
