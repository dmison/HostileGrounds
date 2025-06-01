using UnityEngine;

namespace Health
{
    public class EnemyHealth : MonoBehaviour, IHealthManager
    {
        // Current health of the object
        private int currentHealth;
        public int CurrentHealth { get { return currentHealth; } }

        // Maximum health that the object can have
        [SerializeField]
        protected int maxHealth;
        public int MaxHealth { get { return maxHealth; } }

        // Reference the enemy health bar slider in the UI from the EnemyHealthBar script
        private EnemyHealthBar enemySlider;

        void Start()
        {
            // Set the current health to the max health
            currentHealth = maxHealth;
            // The enemy health bar is a child of the enemy
            enemySlider = GetComponentInChildren<EnemyHealthBar>();
            if (enemySlider != null)
            {
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
            // If the enemy has no more health then it is destroyed
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
        public bool Heal(int healingAmount)
        {
            return false;
        }
    }
}
