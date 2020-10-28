using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;

    public float imunity = 2.5f;    
    public bool imune;
    public GameObject shipShield;

    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthGameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Asteroid" && !imune)
        {
            maxHealth = maxHealth - 1;
            StartCoroutine(ImunityTime());
        }
    }

    IEnumerator ImunityTime()
    {
        imune = true;
        shipShield.gameObject.SetActive(true);
        yield return new WaitForSeconds(imunity);
        shipShield.gameObject.SetActive(false);
        imune = false;
    }

    void HealthGameOver()
    {
        if(maxHealth < 1)
        {
            gameManagerScript.GameOver();
        }
    }
}
