using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButtom : MonoBehaviour
{
    private Button startButtom;
    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        startButtom = GetComponent<Button>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        startButtom.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        gameManagerScript.GameStart();
    }
}
