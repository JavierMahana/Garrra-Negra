using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance { get; private set; }
    void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else { instance = this; }
    }

    public Slider healthBar;
    public float maxHealth = 100;
    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
    }
    
    public void Heal(float heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log($"health: {currentHealth} / {maxHealth}");
        healthBar.value = currentHealth;
    }
    
}
