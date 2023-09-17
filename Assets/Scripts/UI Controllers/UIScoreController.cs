using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIScoreController : MonoBehaviour
{
    public Player playerInfo;

    #region Score
    public TMP_Text scoreText;

    private void Update()
    {
        //Debug.Log("pontuação: "+ playerInfo.pScore);
        scoreText.text = "SCORE: " + playerInfo.pScore.ToString();
    }
    #endregion
}
