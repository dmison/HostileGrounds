using UnityEditor;
using UnityEngine;
using Weapons;

namespace Pickups
{

    public class Pickup : MonoBehaviour
    {
        [SerializeField] public PickupSo pickupData;
        
        [SerializeField] private int pickupAmount;
        [SerializeField] private float respawnDelay = 4f;
        
        private GameObject _pickupPrefab;
        private bool _activePickup = true;
        
        private void Start()
        {
            _pickupPrefab = Instantiate(pickupData.Prefab, transform);
        }
        // When the player triggers this object, call the HandlePlayerPickup method
        private void OnTriggerEnter(Collider col)
        {
            if (!_activePickup || !col.gameObject.CompareTag("Player")) return;
            Debug.Log("The player has triggered a pickup");
            
            GameObject player = col.gameObject;
            bool pickedUp = HandlePlayerPickup(player);
            
            // start respawn if pickup was successful
            if(pickedUp)StartRespawn();
        }

        private void StartRespawn()
        {
            _activePickup = false;
            _pickupPrefab.SetActive(_activePickup);
            Invoke(nameof(ReactivatePickup), respawnDelay);
        }

        private void ReactivatePickup()
        {
            _activePickup = true;
            _pickupPrefab.SetActive(_activePickup);
        }
        
        /// <summary>
        /// HandlePlayerPickup handles all the actions after a player has triggered this object.
        /// </summary>
        /// <param name="player"></param>
        private bool HandlePlayerPickup(GameObject player)
        {
            WeaponsManager wm = player.GetComponent<WeaponsManager>();
            return wm.Pickup(pickupData);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(transform.position, Vector3.one/2);
        }
    }

}
