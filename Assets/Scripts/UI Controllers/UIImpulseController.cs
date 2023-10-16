using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIImpulseController : MonoBehaviour
{
    [SerializeField] private Slider impulseSlider;
    [SerializeField] private Player playerInfo;
    [SerializeField] private TMP_Text impulseNumberIndicator;


    // Update is called once per frame
    void Update()
    {
        impulseSlider.value = playerInfo.currentImpulse;
        impulseNumberIndicator.text = $"{playerInfo.currentImpulse.ToString("F0")}/{playerInfo.maxImpulse.ToString():0}";
    }
    
    public void SetMaxImpulse(float impulse)
    {
        impulseSlider.maxValue = impulse;
        impulseSlider.value = impulse;
        impulseNumberIndicator.text = $"{impulse.ToString()}/{impulse.ToString()}";
    }

    public void SetImpulseBar(float impulse)
    {
        impulseSlider.value = impulse;
    }

}
