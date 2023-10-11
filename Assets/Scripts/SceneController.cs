using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private UIScoreController UIScore;
    private void Awake()
    {
        gameOverUI.SetActive(false);
    }
    public void GameOverScreen()
    {
        Time.timeScale = 0;
        scoreText.text = "TOTAL SCORED: "+ UIScore.currentScore.ToString();
        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //private void LoadNextLevel()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
}
