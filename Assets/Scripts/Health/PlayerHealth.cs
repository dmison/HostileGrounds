using UnityEngine;

namespace Health
{
    /// <summary>
    /// PlayerHealth handles all the health aspects relating to the player
    /// including current health, taking damage, death, and healing
    /// </summary>
    public class PlayerHealth : MonoBehaviour, IHealthManager
    {
        private int currentHealth;
        public int CurrentHealth => currentHealth;

        [SerializeField]
        protected int maxHealth;
        public int MaxHealth => maxHealth;

        void Start()
        {
            currentHealth = maxHealth;
            UpdateUI();
        }
        private void UpdateUI()
        {
            // avoiding divide by zero errors
            if (maxHealth <= 0)
            {
                Debug.LogError("Max Health should never be less or equal to zero");
                return;
            }
            UIManager.Instance.CurrentHealthPercent = (float)currentHealth / (float)maxHealth * 100f;
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
            UpdateUI();
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
            UpdateUI();
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
            GameOver.TriggerGameOver();
        }
    }
}
