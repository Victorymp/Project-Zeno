using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleshipHealth : MonoBehaviour
{
    public Slider healthSlider;
    public float health;
    void update()
    {
        healthSlider.value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
    }

    public void Heal(float heal)
    {
        health += heal;
        healthSlider.value = health;
    }

    public void SetHealth(float newHealth)
    {
        health = newHealth;
        healthSlider.value = health;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        health = maxHealth;
    }
}
