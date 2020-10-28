using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Caixas de texto
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI requiredScoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI healthText;

    //Menus
    public GameObject title;
    public GameObject startGameButtons;
    public GameObject gameOverText;
    public GameObject restartGameButton;
    public GameObject pauseTitle;
    public GameObject exitPauseBMenu;
    public GameObject optionMenu;
    public GameObject controls;

    //Controle de pontuação
    public int playerCargo;
    public int maxCargoSpace = 100;
    public int refinaryCargoScore = 0;
    public int refinaryLimitScore = 1500;

    //Controle de tempo 
    public float timeLeft;

    //Controle do jogo
    public bool gameStart;
    public bool gameOver;
    public bool isPauseActive;
    public bool isOptionActive;
    public bool isControlOpen;

    //Jogador
    public GameObject player;
    public PlayerHealth playerHealthScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimeCount();
        PauseMenu();
        HealthUI();
    }

    public void UpdateScore(int scoreToAdd)
    {
        if(playerCargo >= maxCargoSpace)
        {
            scoreToAdd = 0;
        }
        playerCargo += scoreToAdd;
        scoreText.text = "Player Cargo: " + playerCargo + " / " + maxCargoSpace;
    }

    public void RefinaryScore(int scoreToAdd)
    {
        refinaryCargoScore += scoreToAdd;
        requiredScoreText.text = "Required Cargo in Refinary: " + refinaryCargoScore + " / " + refinaryLimitScore;
    }

    public void HealthUI()
    {
        if(gameStart == true)
        {
            playerHealthScript = GameObject.Find("Player").GetComponent<PlayerHealth>();
            healthText.gameObject.SetActive(true);
            healthText.text = "Health: " + playerHealthScript.maxHealth;
        }
    }

    public void GameStart()
    {
        gameStart = true;
        player.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        requiredScoreText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        UpdateScore(0);
        RefinaryScore(0);
        TimeCount();
        HealthUI();
        title.gameObject.SetActive(false);
        startGameButtons.gameObject.SetActive(false);
    }

    public void GameOption()
    {
        if(!isOptionActive)
        {
            optionMenu.gameObject.SetActive(true);
            isOptionActive = true;
        }
        else if(isOptionActive)
        {
            optionMenu.gameObject.SetActive(false);
            isOptionActive = false;
        }
    }

    public void GameControl()
    {
        if (!isControlOpen)
        {
            controls.gameObject.SetActive(true);
            isControlOpen = true;
        }
        else if(isControlOpen)
        {
            controls.gameObject.SetActive(false);
            isControlOpen = false;
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartGameButton.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        gameStart = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseMenu()
    {
        //Pause
        if(gameStart == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isPauseActive == false)
            {
                isPauseActive = true;
                pauseTitle.gameObject.SetActive(true);
                exitPauseBMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isPauseActive == true)
            {
                isPauseActive = false;
                pauseTitle.gameObject.SetActive(false);
                exitPauseBMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }   
    }

    private void TimeCount()
    {
        if(gameStart == true)
        {
            timeLeft -= Time.deltaTime;
            timeText.text = "Time Left: " + Mathf.Round(timeLeft);
            if(timeLeft <= 0)
            {
                GameOver();
            }
        }
        
    }
}
