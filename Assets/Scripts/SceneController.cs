using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private UIScoreController UIScore;
    [SerializeField] private PlayableDirector deathCutscene;
    [SerializeField] private GameObject cutsceneContainer;

    #region Passing status variables between scenes
    private UIScoreController scoreAndXp;
    private Player player;
    private PlayerShield shield;
    #endregion

    private void Awake()
    {
        scoreAndXp = FindAnyObjectByType<UIScoreController>();
        player = FindAnyObjectByType<Player>();
        shield = FindAnyObjectByType<PlayerShield>();
        LoadDataState();

        cutsceneContainer.SetActive(false);

        gameOverUI.SetActive(false);
        GameEvents.current.onTimerFinishedTrigger += LoadNextLevel;
        GameEvents.current.onPlayerDeathTrigger += DeathScreens;
    }
    private void DeathScreens()
    {
        PlayGameOverCutscene();
        Invoke("GameOverScreen", 1.5f);
    }

    private void PlayGameOverCutscene()
    {
        cutsceneContainer.SetActive(true);
        deathCutscene.Play();
    }

    private void GameOverScreen()
    {
        Time.timeScale = 0;
        scoreText.text = "TOTAL SCORED: " + UIScore.currentScore.ToString();
        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SavingDataState();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        Debug.Log($"Fase: {levelIndex}");
        yield return new WaitForSeconds(1f);
        //Chamar transição de fase aqui
        SceneManager.LoadSceneAsync(levelIndex);
    }

    private void SavingDataState()
    {
        //Salva os dados de status que serão passados pra proxima fase
        StatusDataController.currentHealth = player.currentHealth;
        StatusDataController.shootCooldown = player.shootCooldown;
        StatusDataController.maxImpulse = player.maxImpulse;
        StatusDataController.increaseImpulse = player.increaseImpulseVariantValue;
        StatusDataController.decreaseImpulse = player.decreaseImpulseVariantValue;

        StatusDataController.currentScore = scoreAndXp.currentScore;
        StatusDataController.currentXp = scoreAndXp.currentXp;

        StatusDataController.shieldActiveTime = shield.shieldActiveTime;
        StatusDataController.shieldRechargeTime = shield.shieldRechargeTime;
    }

    private void LoadDataState()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0)
        {
            //Quando a fase começa os status salvos são carregados aqui.
            player.currentHealth = StatusDataController.currentHealth;
            player.shootCooldown = StatusDataController.shootCooldown;
            player.maxImpulse = StatusDataController.maxImpulse;
            player.increaseImpulseVariantValue = StatusDataController.increaseImpulse;
            player.decreaseImpulseVariantValue = StatusDataController.decreaseImpulse;

            scoreAndXp.currentScore = StatusDataController.currentScore;
            scoreAndXp.currentXp = StatusDataController.currentXp;

            shield.shieldActiveTime = StatusDataController.shieldActiveTime;
            shield.shieldRechargeTime = StatusDataController.shieldRechargeTime;
        }
    }
}