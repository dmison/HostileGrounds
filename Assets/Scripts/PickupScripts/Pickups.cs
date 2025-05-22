using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PickupScripts
{

    public class Pickups : MonoBehaviour
    {
        // Select the pickup that corresponds to the weapon type
        [SerializeField]
        private TypeOfPickup pickupType;

        // The amount that the pickup will add to the players inventoy
        [SerializeField]
        private int pickupAmount;

        // Reference to the pickup spawer
        private PickupSpawner spawner;

        // Assign the pickup to spawn from the PickupSpawner
        public void AssignSpawner(PickupSpawner assignedSpawner)
        {
            spawner = assignedSpawner;
        }

        // When the player triggers this object, call the HandlePlayerPickup method
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                GameObject player = col.gameObject;
                Debug.Log("The player has triggered a pickup");
                HandlePlayerPickup(player);
                // Start the respawn countdown that is within the PickupSpawner
                spawner?.StartRespawnCountdown();
            }
        }

        /// <summary>
        /// HandlePlayerPickup handles all of the actions after a player has triggered this object.
        /// It grabs the IWeapon component from the player, transfers all information to a new IWeapon (based on the provided pickupType).
        /// </summary>
        /// <param name="player"></param>
        private void HandlePlayerPickup(GameObject player)
        {
            if (pickupType == TypeOfPickup.Pistol)
            {
                // Increase the amount of ammo by the pickupAmount
                Debug.Log("Pistol ammo increases by: " + pickupAmount);
                // IncreasePistolAmmo(pickupAmount);
            }
            // If SMG ammo is 'picked up'
            if (pickupType == TypeOfPickup.SMG)
            {
                // Increase the amount of ammo by the pickupAmount
                Debug.Log("SMG ammo increases by: " + pickupAmount);
                // IncreaseSMGAmmo(pickupAmount);
            }
            // If a grenade is 'picked up'
            if (pickupType == TypeOfPickup.Grenade)
            {
                // Increase the amount of grenades by the pickupAmount
                Debug.Log("Grenade amount increases by: " + pickupAmount);
                // IncreaseGrenadeAmmo(pickupAmount);
            }
            // If a grenade is 'picked up'
            if (pickupType == TypeOfPickup.Health)
            {
                // Increase the amount of health by the pickupAmount
                Debug.Log("Health amount increases by: " + pickupAmount);
                // IncreaseHealth(pickupAmount);
            }
        }
    }

}
