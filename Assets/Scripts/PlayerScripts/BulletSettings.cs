using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSettings : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 100;
    private AsteroidHealth asteroidHealthScript;
    private MiniAsteroidHealth miniAsteroidHealthScript;
    public float lifeSpan = 1.5f;
    public int bulletDamage = 1;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        DestroyAfterTime();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Instantiate(explosion, transform.position, transform.rotation);
        if (collision.gameObject.tag.Equals("Asteroid")) 
        {
            asteroidHealthScript = collision.gameObject.GetComponent<AsteroidHealth>();
            asteroidHealthScript.currentHealth -= bulletDamage;
        }

        if(collision.gameObject.tag.Equals("MiniAsteroid"))
        {
            miniAsteroidHealthScript= collision.gameObject.GetComponent<MiniAsteroidHealth>();
            miniAsteroidHealthScript.currentHealth -= bulletDamage;
        }
        Destroy(gameObject);
    }

    void Movement()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void DestroyAfterTime()
    {
        Destroy(gameObject, lifeSpan);
    }

}
