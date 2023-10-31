using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using static Cinemachine.DocumentationSortingAttribute;

public class UIScoreController : MonoBehaviour
{
    public TMP_Text scoreText;
    [SerializeField] private TMP_Text XPLevelText;
    private int XPLevel;
    public int currentScore = 0;
    public int currentXp = 0;
    private int maxXp = 10000;
    public Slider xpSlider;
    private int recordScore;

    private void Awake()
    {
        XPLevel = 0;
        recordScore = PlayerPrefs.GetInt("record");    
    }

    private void Start()
    {
        XPLevelText.text = "Lvl. " + XPLevel.ToString();
        xpSlider.maxValue = maxXp;
        xpSlider.value = currentXp;
        GameEvents.current.onPlayerScoredTrigger += OnPlayerScored;
    }

    private void OnPlayerScored(int score) //Logica do evento
    {
        currentScore += score;
        currentXp += score;

        if(currentXp > maxXp)
        {
            Debug.Log("LEVEL UP!");
            //Chamar tela de UPGRADE
            GameEvents.current.PlayerUpgrade();

            XPLevel++;
            ResetMaxXp(maxXp + maxXp / 2); //Trocar essa formula par auma mais balanceada.
        }
        else
        {
            SetXp(currentXp);
        }
    }

    private void ResetMaxXp(int maxXp) //O maximo da barra de xp reseta e aumenta assim que o player chegar no seu limite maximo.
    {
        xpSlider.maxValue = maxXp;
        currentXp = 0;
        xpSlider.value = currentXp;
        this.maxXp = maxXp;
    }

    private void SetXp(int xp)
    {
        xpSlider.value = xp;
    }

    private void Update()
    {
        scoreText.text = "SCORE: " + currentScore.ToString();
        XPLevelText.text = "Lvl. " + XPLevel.ToString();
    }

    public bool SetNewRecord()
    {
        if(currentScore > recordScore)
        {
            recordScore = currentScore;
            PlayerPrefs.SetInt("record", this.recordScore);
            return true;
        }
        return false;
    }
}
