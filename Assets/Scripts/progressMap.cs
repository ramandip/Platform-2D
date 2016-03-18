using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class progressMap : MonoBehaviour {
    public int totalScores;
    private int totalLives;

    public int ScoreValue
    {
        get
        {
            return totalScores;
        }

        set
        {
            totalScores = value;
            this.scoreLabel.text = "Score: " + this.totalScores;
        }
    }


    public int livesValue
    {
        get
        {
            return totalLives;
        }
        set
        {
            totalLives = value;
            if (totalLives < 0) { endGame(); }
            else {
                this.livesLabel.text = "Lives:" + totalLives;
            }
        }

    }
    // PUBLIC INSTANCE VARIABLES
    
    public coin _coin;
    public Text livesLabel;
    public Text scoreLabel;
    public Text GameOverLabel;
    public Text WinLabel;
    public Text HighScoreLabel;
    public Button RestartButton;

    public void endGame()
    {
        HighScoreLabel.text = "High Score: " + this.totalScores;
        GameOverLabel.gameObject.SetActive(true);
        WinLabel.enabled = false;
        HighScoreLabel.gameObject.SetActive(true);
        livesLabel.gameObject.SetActive(false);
        scoreLabel.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(true);

    }

    // Use this for initialization
    void Start () {
        initializer();
	}

    private void initializer()
    {
        ScoreValue = 0;
        livesValue = 2;
        GameOverLabel.gameObject.SetActive(false);
        WinLabel.gameObject.SetActive(false);
        WinLabel.enabled = false;
        HighScoreLabel.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        livesLabel.text = "Lives: " + livesValue;
        scoreLabel.text = "Score: " + ScoreValue;
	}
    public void RestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void finish()
    {

        HighScoreLabel.text = "High Score: " + this.totalScores;
        GameOverLabel.gameObject.SetActive(false);
        WinLabel.enabled = true;
        HighScoreLabel.gameObject.SetActive(true);
        livesLabel.gameObject.SetActive(false);
        scoreLabel.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(true);
    }
}

