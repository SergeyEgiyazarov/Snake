using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text cristalsText;
    public Text totalScoreText;

    public GameObject gameOverPanel;
    public GameObject finishPanel;

    private static UIManager _instance;

    public static UIManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        Singelton();
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

    public void UpdateScore(int score)
    {
        scoreText.text = "" + score;
    }

    public void UpdateCristals(int cristals)
    {
        cristalsText.text = "" + cristals;
    }

    public void EndGameAcive(int totalScore)
    {
        totalScoreText.text = "Score: " + totalScore;
        finishPanel.SetActive(true);
    }

    public void GameOverActive()
    {
        gameOverPanel.SetActive(true);
    }
}
