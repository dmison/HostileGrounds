using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerHealth handles all of the health aspects relating to the player
/// including damage, death, healing, and a health bar
/// </summary>
public class PlayerHealth : MonoBehaviour, HealthManager
{
    [SerializeField]
    protected int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    [SerializeField]
    protected int maxHealth;
    public int MaxHealth { get { return maxHealth; } }

    void Start()
    {
        currentHealth = maxHealth;
        UIManager.instance.UpdatePlayerHealthSlider((float)currentHealth / (float)maxHealth);
    }

    /// <summary>
    /// Heal handles the functionality of receiving health
    /// </summary>
    /// <param name="healingAmount">The amount of health to gain, this value should be positive</param>
    public void Heal(int healingAmount)
    {
        // Increase the current health by the set healing amount
        currentHealth += healingAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        // Update the player health bar in the UI
        UIManager.instance.UpdatePlayerHealthSlider((float)currentHealth / (float)maxHealth);
    }

    /// <summary>
    /// TakeDamage handles the functionality for taking damage
    /// </summary>
    /// <param name="damageAmount">The amount of damage to lose, this value should be positive</param>
    public void TakeDamage(int damageAmount)
    {
        // Decrease the current health by the damage amount
        currentHealth -= damageAmount;
        // Update the player health bar in the UI
        UIManager.instance.UpdatePlayerHealthSlider((float)currentHealth / (float)maxHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    /// <summary>
    /// Die handles all functionality related to when health reaches or goes below zero, should perform all necessary cleanup.
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
    }
}
