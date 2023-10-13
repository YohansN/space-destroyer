using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    public Player playerInfo;

    #region Health
    //Health Bar
    public Slider healthSlider;

    public void SetMaxHealth(int maxHealth, int currentHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }
    #endregion
}
