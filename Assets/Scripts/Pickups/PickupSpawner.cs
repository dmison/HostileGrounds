using UnityEngine;

namespace Pickups
{
    public class PickupSpawner : MonoBehaviour
    {
        // Object to spawn in the spawner
        [SerializeField]
        private GameObject pickupPrefab;

        // Delay time between respawn
        [SerializeField]
        private float respawnDelay = 4f;

        // The current pickup that has spawned
        private GameObject currentPickup;

        // On Start of game, Spawn the object into the scene
        void Start()
        {
            Spawn();
        }

        // Spawn the selected pickup from the Pickups script within the spawner
        void Spawn()
        {
            currentPickup = Instantiate (pickupPrefab, transform.position, Quaternion.identity);
            Pickup pickupScript = currentPickup.GetComponent<Pickup>();
            // If there is a pickup script set the pickup to this spawner
            if (pickupScript != null)
            {
                // pickupScript.AssignSpawner(this);
            }
        }

        // Destroy the pickup and start a countdown of respawnDelay before respawning the pickup again
        public void StartRespawnCountdown()
        {
            if (currentPickup != null)
            {
                Destroy(currentPickup);
            }
            Invoke(nameof(Spawn), respawnDelay);
        }
    }
}
