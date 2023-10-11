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
        GameEvents.current.onTimerFinishedTrigger += LoadNextLevel;
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
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        Debug.Log($"Fase: {levelIndex}");
        yield return new WaitForSeconds(1f);
        //Chamar transição de fase aqui
        SceneManager.LoadSceneAsync(levelIndex);
    }
}
