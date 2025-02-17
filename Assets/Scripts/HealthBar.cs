using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth; // Inicia la barra llena
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health; // Ajusta la barra al valor de vida actual
    }
}