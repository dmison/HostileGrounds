namespace Health
{
    /// <summary>
    /// HealthManager handles all of the Health components available
    /// </summary>
    public interface IHealthManager
    {
        // Get the amount of health this component currently has
        int CurrentHealth { get; }
        // Get the maximum health of this component
        int MaxHealth { get; }

        /// <summary>
        /// TakeDamage handles the functionality for taking damage
        /// </summary>
        /// <param name="damageAmount">The amount of damage to lose, this value should be positive</param>
        void TakeDamage(int damageAmount);

        /// <summary>
        /// Heal handles the functionality of receiving health
        /// </summary>
        /// <param name="healingAmount">The amount of health to gain, this value should be positive</param>
        void Heal(int healingAmount);

        /// <summary>
        /// Die handles all functionality related to when health reaches or goes below zero, should perform all necessary cleanup.
        /// </summary>
        void Die();
    }
}
