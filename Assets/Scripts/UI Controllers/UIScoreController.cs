using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIScoreController : MonoBehaviour
{
    public TMP_Text scoreText;
    private int currentScore = 0;
    private int currentXp = 0;
    private int maxXp = 10000;
    public Slider xpSlider;

    private void Start()
    {
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
    }
}
