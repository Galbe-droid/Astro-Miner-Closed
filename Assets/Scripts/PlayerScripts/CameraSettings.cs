using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public GameObject follow;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);
    private GameManager gameManagerScript;
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
            transform.position = follow.transform.position - offset;
        }
        else
        {
            transform.position = new Vector3(0, 80, 0);
        }
    }
}
