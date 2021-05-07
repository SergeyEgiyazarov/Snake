using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameTime;
    public SpawnManager spawnManager;
    public GameObject gameOverPanel;
    public GameObject finishPanel;
    public Text scoreText;

    private static GameManager _instance;
    private bool isGameEnd = false;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        Singelton();
    }

    private void Update()
    {
        gameTime -= Time.deltaTime;

        if (gameTime <= 0.0f && !isGameEnd)
        {
            EndGame();
            isGameEnd = true;
        }
    }

    private void Singelton()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void EndGame()
    {
        spawnManager.GameFinish();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void Finish(int score)
    {
        scoreText.text = "Score: " + score;
        finishPanel.SetActive(true);
    }

    public void RestatrGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
