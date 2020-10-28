using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreBehavior : MonoBehaviour
{
    private Rigidbody oreRb;
    private float yLimit = 6f;
    public GameObject spark;
    private bool sparkTime = false;
    private GameManager gameManagerScript;
    public float lifeSpawn = 5f;
    public float lifeTime = 0f;

    //Pontuação
    public int addScore;

    //Circulo Delimitador 
    float radius = 200f;
    Vector3 centerPosition = new Vector3(0, 6, 0);

    // Start is called before the first frame update
    void Awake()
    {
        oreRb = GetComponent<Rigidbody>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        LimitZone();
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        Destruction();
    }

    void Destruction()
    {
        if(lifeTime >= lifeSpawn)
        {
            int addScoreHalfed = addScore / 2;
            gameManagerScript.RefinaryScore(addScoreHalfed);
            sparkTime = true;
            SpawnSpark();
            Destroy(gameObject);
        }      
    }

    void LimitZone()
    {
        float distance = Vector3.Distance(oreRb.transform.position, centerPosition);

        if (distance > radius)
        {
            Vector3 radiusLimit = oreRb.transform.position - centerPosition;
            radiusLimit *= radius / distance;
            oreRb.transform.position = centerPosition + radiusLimit;
        }

        if (transform.position.y == yLimit)
        {
            transform.Translate(transform.position.x, yLimit, transform.position.z);
        }
    }

    void SpawnSpark()
    {
        if(sparkTime == true)
        {
            Instantiate(spark, transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManagerScript.UpdateScore(addScore);
            sparkTime = true;
            SpawnSpark();
            Destroy(gameObject);
        }
    }
}
