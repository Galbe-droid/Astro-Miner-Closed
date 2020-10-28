using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAsteroidHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    public int currentHealth;
    private SpawnManager spawnManagerScript;
    private bool collisionMagnet = false;

    public GameObject ore;

    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyMiniAsteroid();
    }

    void DestroyMiniAsteroid()
    {
        if (gameObject.tag.Equals("MiniAsteroid") && currentHealth < 1 && collisionMagnet == false)
        {
            spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

            spawnManagerScript.miniAsteroidDestroy++;
            float randomPos = Random.Range(-5, 5);
            Instantiate(ore, transform.position + new Vector3(randomPos, 0, randomPos), transform.rotation);
            Destroy(gameObject);
        }
    }

    void Absorption()
    {
        if (gameObject.tag.Equals("MiniAsteroid") && currentHealth < 1)
        {
            spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
            gameManagerScript.RefinaryScore(5);
            spawnManagerScript.miniAsteroidDestroy++;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Magnet"))
        {
            currentHealth--;
            collisionMagnet = true;
            Absorption();
        }
    }
}
