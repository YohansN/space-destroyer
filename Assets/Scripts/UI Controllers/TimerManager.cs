using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private int minutes;
    [SerializeField] private int seconds;
    private TMP_Text timerText;
    private float currentTime;
    private bool hasBeenCalled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TMP_Text>();
        currentTime = minutes * 60 + seconds;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if(currentTime > 0)
        {
            int updatedMinutes = Mathf.FloorToInt(currentTime / 60);
            int updatedSeconds = Mathf.FloorToInt(currentTime % 60);

            timerText.text = $"{updatedMinutes:00}:{updatedSeconds:00}";

            currentTime -= Time.deltaTime;
        }
        else
        {
            //Fim da wave
            timerText.text = "00:00";
            if(hasBeenCalled == false)
            {
                hasBeenCalled = true;
                GameEvents.current.TimerFinishedTrigger(); //Evento que faz o jogo trocar de fase/cena
            }
        }
    }
}
