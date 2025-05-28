using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, HealthManager
{
    // Current health of the object
    [SerializeField]
    protected int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    // Maximum health that the object can have
    [SerializeField]
    protected int maxHealth;
    public int MaxHealth { get { return maxHealth; } }

    // Reference the enemy health bar slider in the UI from the EnemyHealthBar script
    private EnemyHealthBar enemySlider;

    void Start()
    {
        // Set the current health to max health
        enemySlider = GetComponentInChildren<EnemyHealthBar>();
        if (enemySlider != null)
        {
            currentHealth = maxHealth;
            enemySlider.UpdateHealth(currentHealth, maxHealth);
        }
        else
        {
            Debug.Log("Enemy health slider is not assigned to the enemy!");
        }
    }

    /// <summary>
    /// TakeDamage handles the functionality for taking damage
    /// </summary>
    /// <param name="damageAmount">The amount of damage to lose, this value should be positive</param>
    public void TakeDamage(int damageAmount)
    {
        // The amount of damage taken is subtracted from the current health
        currentHealth -= damageAmount;
        // The health bar in the UI is updated
        enemySlider.UpdateHealth(currentHealth, maxHealth);
        // If the object has no more health then it is destroyed
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
        // Remove this object from the game
        Destroy(gameObject);
    }

    /// The Heal method is required due to inheriting from the IHealth interface however the enemy does not heal, although it has the capabilities to do so.
    public void Heal(int healingAmount)
    {
        // Do nothing because the enemy is not meant to heal, however if the feature was desired the script below is a starting point to implement that feature
        /// currentHealth += healingAmount;
        /// enemySlider.UpdateHealth(currentHealth, maxHealth);
        /// if (currentHealth > maxHealth)
        /// {
        ///     currentHealth = maxHealth;
        /// }
    }
}
