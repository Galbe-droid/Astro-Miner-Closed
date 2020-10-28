using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPosition;
    public GameObject asteroid;
    private GameManager gameManagerScript;

    public float spawnDelay = 5;
    private float spawnTime;

    public float asteroidLimit = 10;
    public float asteroidQuantity;

    public float miniAsteroidDestroy = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.gameStart == true)
        {
            spawnTime += Time.deltaTime;

            if (spawnTime >= spawnDelay && asteroidQuantity < asteroidLimit)
            {
                AsteroidSpawn();
            }
            DestoyedMiniAsteroid();
        }   
    }

    void AsteroidSpawn()
    {
        int spawnRandom = Random.Range(0, spawnPosition.Length);
        Vector3 xRandom = new Vector3(Random.Range(-100, 100), spawnPosition[spawnRandom].transform.position.y, spawnPosition[spawnRandom].transform.position.z);

        Vector3 zRandom = new Vector3(spawnPosition[spawnRandom].transform.position.x, spawnPosition[spawnRandom].transform.position.y, Random.Range(-100, 100));

        if(spawnRandom < 2)
        {
            Instantiate(asteroid, xRandom, spawnPosition[Random.Range(0, spawnPosition.Length)].transform.rotation);
        }
        else
        {
            Instantiate(asteroid, zRandom, spawnPosition[Random.Range(0, spawnPosition.Length)].transform.rotation);
        }

        spawnTime = 0;
        asteroidQuantity++;
    }

    void DestoyedMiniAsteroid()
    {
        if(miniAsteroidDestroy >= 4)
        {
            asteroidQuantity--;
            miniAsteroidDestroy = 0;
        }
    }
}
