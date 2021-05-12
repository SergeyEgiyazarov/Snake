using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameTime;
    public SpawnManager spawnManager;
    public UIManager uiManager;
    public SnakeController snake;

    private static GameManager _instance;
    private bool isGameEnd = false;

    private int score = 0;
    private int cristals = 0;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        Singelton();
    }

    private void Start()
    {
        snake.SnakeEatingHuman += Snake_SnakeEatingHuman;
        snake.SnakeEatingCristal += Snake_SnakeEatingCristal;
        snake.EndFaver += Snake_EndFaver;
    }

    private void Update()
    {
        gameTime -= Time.deltaTime;

        if (gameTime <= 0.0f && !isGameEnd)
        {
            EndGame();
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

    private void Snake_SnakeEatingHuman()
    {
        score++;
        uiManager.UpdateScore(score);
    }

    private void Snake_SnakeEatingCristal()
    {
        cristals++;
        uiManager.UpdateCristals(cristals);
        if (cristals > 3)
        {
            snake.StartFaver();
        }
    }

    private void Snake_EndFaver()
    {
        cristals = 0;
        uiManager.UpdateCristals(cristals);
    }

    private void EndGame()
    {
        spawnManager.GameFinish();
        isGameEnd = true;
    }

    public void GameOver()
    {
        uiManager.GameOverActive();
        EndGame();
    }

    public void Finish()
    {
        uiManager.EndGameAcive(score);
        EndGame();
    }

    public void RestatrGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
