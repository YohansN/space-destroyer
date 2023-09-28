using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIScoreController : MonoBehaviour
{
    public TMP_Text scoreText;
    private int totalScore = 0;

    private void Start()
    {
        GameEvents.current.onPlayerScoredTrigger += OnPlayerScored;
    }

    private void OnPlayerScored(int score) //Logica do evento
    {
        totalScore += score;
    }

    private void Update()
    {
        scoreText.text = "SCORE: " + totalScore.ToString();
    }
}
