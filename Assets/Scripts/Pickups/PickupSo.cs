using UnityEngine;

namespace Pickups
{
    [CreateAssetMenu(fileName = "Pickup", menuName = "HostileGrounds/Pickup")]
    public class PickupSo : ScriptableObject
    {
        public PickupData pickupData;   
        
        public int pickupAmount;
        public float respawnDelay = 4f;
        
        public TypeOfPickup PickupType => pickupData.pickupType;
        public GameObject Prefab => pickupData.prefab;
    }
}
