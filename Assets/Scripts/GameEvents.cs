using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    #region Score Event
    public event Action<int> onPlayerScoredTrigger;
    public void PlayerScoredTrigger(int score)
    {
        if (onPlayerScoredTrigger != null) //Verifica se tem uma atibuição de método para o evento antes de chamalo.
            onPlayerScoredTrigger(score);
    }
    #endregion
}
