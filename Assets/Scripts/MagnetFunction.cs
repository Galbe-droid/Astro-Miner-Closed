using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetFunction : MonoBehaviour
{
    //Repulsão
    private Vector3 magnetRepulsion;
    private Vector3 repulsionLocation;
    private Vector3 repulsionPlayer;
    public GameObject player;
    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 1 * 15 * Time.deltaTime, 0);
        if (gameManagerScript.gameStart == true)
        {
            player = GameObject.Find("Player");
            repulsionPlayer = new Vector3(player.transform.position.x, 6, player.transform.position.z);
            Repulsion();
        }
    }

    void Repulsion()
    {
        magnetRepulsion = (repulsionLocation - repulsionPlayer).normalized;

        player.transform.position = repulsionPlayer - magnetRepulsion * 0.1f;
    }
}
