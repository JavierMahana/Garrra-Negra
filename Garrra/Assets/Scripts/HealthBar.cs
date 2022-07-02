using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth = 100;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKey("q")) TakeDamage(100);
        if (currentHealth <= 0)
        {
            Heal(100);
            SceneController.instance.Restart();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) currentHealth = 0;
        healthBar.value = currentHealth;

        Debug.Log($"health: {currentHealth} / {maxHealth}");
    }
    
    public void Heal(float heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthBar.value = currentHealth;

        Debug.Log($"health: {currentHealth} / {maxHealth}");
    }
    
}
