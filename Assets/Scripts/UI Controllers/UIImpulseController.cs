using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImpulseController : MonoBehaviour
{
    public Slider impulseSlider;
    public Player playerInfo;


    // Update is called once per frame
    void Update()
    {
        impulseSlider.value = playerInfo.currentImpulse;
    }
    
    public void SetMaxImpulse(float impulse)
    {
        impulseSlider.maxValue = impulse;
        impulseSlider.value = impulse;
    }

    public void SetImpulseBar(float impulse)
    {
        impulseSlider.value = impulse;
    }

}
