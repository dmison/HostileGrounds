using UnityEngine;
using UnityEngine.Serialization;

namespace Pickups
{
    public abstract class PickupData : ScriptableObject
    {
        public TypeOfPickup pickupType;
        public GameObject prefab;
    }
}