using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text recordScoreText;
    [SerializeField] private UIScoreController UIScore;
    [SerializeField] private PlayableDirector deathCutscene;
    [SerializeField] private GameObject cutsceneContainer;
    [SerializeField] private Animator transition;
    [SerializeField] private AudioSource clickSound;

    #region Passing status variables between scenes
    private UIScoreController scoreAndXp;
    private Player player;
    private PlayerShield shield;
    private UpgradeManager upgradeManager;
    #endregion

    private readonly int firstLevelIndex = 2;

    private void Awake()
    {
        scoreAndXp = FindAnyObjectByType<UIScoreController>();
        player = FindAnyObjectByType<Player>();
        shield = FindAnyObjectByType<PlayerShield>();
        upgradeManager = FindAnyObjectByType<UpgradeManager>();
        
        if(SceneManager.GetActiveScene().buildIndex != firstLevelIndex)
            LoadDataState();

        cutsceneContainer.SetActive(false);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);

        GameEvents.current.onPlayerPauseTrigger += PauseScreen;
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
        bool isNewRecord = UIScore.SetNewRecord();
        Time.timeScale = 0;
        scoreText.text = "TOTAL SCORED: " + UIScore.currentScore.ToString();
        if (isNewRecord)
            recordScoreText.text = "NEW RECORD: " + PlayerPrefs.GetInt("record").ToString() + "!";
        else
            recordScoreText.text = "RECORD: " + PlayerPrefs.GetInt("record").ToString();
        gameOverUI.SetActive(true);
    }

    #region Pause Screen Options
    private void PauseScreen()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }
    //Opções do Menu de pausa:
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        clickSound.Play();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        clickSound.Play();
        transition.SetTrigger("Start");
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        clickSound.Play();
        transition.SetTrigger("Start");
        SceneManager.LoadScene(firstLevelIndex);
    }
    #endregion

    private void LoadNextLevel()
    {
        SavingDataState();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        Debug.Log($"Fase: {levelIndex}");
        //Chamar transição de fase aqui
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
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
        StatusDataController.XPLevel = scoreAndXp.XPLevel;
        StatusDataController.maxXp = scoreAndXp.maxXp;

        StatusDataController.shieldActiveTime = shield.shieldActiveTime;
        StatusDataController.shieldRechargeTime = shield.shieldRechargeTime;

        StatusDataController.AllUpgrades = upgradeManager.AllUpgrades;
    }

    private void LoadDataState()
    {
        if(SceneManager.GetActiveScene().buildIndex >= firstLevelIndex)
        {
            //Quando a fase começa os status salvos são carregados aqui.
            player.currentHealth = StatusDataController.currentHealth;
            player.shootCooldown = StatusDataController.shootCooldown;
            player.maxImpulse = StatusDataController.maxImpulse;
            player.increaseImpulseVariantValue = StatusDataController.increaseImpulse;
            player.decreaseImpulseVariantValue = StatusDataController.decreaseImpulse;

            scoreAndXp.currentScore = StatusDataController.currentScore;
            scoreAndXp.currentXp = StatusDataController.currentXp;
            scoreAndXp.XPLevel = StatusDataController.XPLevel;
            scoreAndXp.maxXp = StatusDataController.maxXp;

            shield.shieldActiveTime = StatusDataController.shieldActiveTime;
            shield.shieldRechargeTime = StatusDataController.shieldRechargeTime;

            upgradeManager.AllUpgrades = StatusDataController.AllUpgrades;
        }
    }
}