using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthNumberIndicator;

    public void SetMaxHealth(int maxHealth, int currentHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        healthNumberIndicator.text = $"{currentHealth.ToString()}/{maxHealth.ToString()}";
    }

    public void SetHealth(int health, int maxHealth)
    {
        healthSlider.value = health;
        healthNumberIndicator.text = $"{health.ToString()}/{maxHealth.ToString()}";
    }
}
