using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    public int currentHealth;
    public GameObject miniAsteroid;



    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        DestructionAsteroid();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Magnet"))
        {
            currentHealth--;
        }
    }

    //Asteroid
    void DestructionAsteroid()
    {
        if (currentHealth < 1)
        {
            for (int i = 0; i < 4; i++)
            {
                float randomPos = Random.Range(-10, 10);
                Instantiate(miniAsteroid, transform.position + new Vector3(randomPos, 0, randomPos), transform.rotation);
            }
            Destroy(gameObject);
        }
    }  
}
