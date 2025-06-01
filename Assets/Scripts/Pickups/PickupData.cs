using UnityEngine;

namespace Pickups
{
    public abstract class PickupData : ScriptableObject
    {
        public GameObject prefab;
        public GameObject Prefab => prefab;

        public virtual bool Execute(GameObject target, int amount)
        {
            return false;
        }
    }
}