using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public Text bestScore;
    public Text endScoreText;
    public Text endBestScore;
    public Text newHighScore;
    public GameObject gameOverScreen;
    public AudioSource PointSFX;
    public AudioSource HighScoreSFX;
    public AudioSource FailSFX;
    public AudioSource music;
    public bool gameEnded = false;

    void Start()
    {
        music.Play();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd, int highScore)
    {
        PointSFX.Play();
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();
        if(playerScore > highScore)
        {
            bestScore.text = playerScore.ToString();
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }

    public void gameOver(bool gameEnded)
    {
        if (gameEnded)
        {
            music.Stop();
            endScoreText.text = playerScore.ToString();
            endBestScore.text = bestScore.text;
            if (GameObject.FindGameObjectWithTag("GameUI") && GameObject.FindGameObjectWithTag("GameUI").activeInHierarchy) GameObject.FindGameObjectWithTag("GameUI").SetActive(false);
            if (playerScore > PlayerPrefs.GetInt("HighScore"))
            {
                HighScoreSFX.Play();
                PlayerPrefs.SetInt("HighScore", playerScore);
                newHighScore.enabled = true;
            }
            else
            {
                FailSFX.Play();
            }
            gameOverScreen.SetActive(true);
        }
        gameEnded = false;
    }

    public void setHighScore(int highScore)
    {
        bestScore.text = highScore.ToString();
    }

    public void gameExit()
    {
        Application.Quit();
    }
}
