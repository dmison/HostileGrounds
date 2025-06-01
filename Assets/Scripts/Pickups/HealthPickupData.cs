using Health;
using UnityEngine;

namespace Pickups
{
    [CreateAssetMenu(fileName = "HealthPickup", menuName = "HostileGrounds/Pickups/HealthPickup")]
    public class HealthPickupData : PickupData
    {
        public int healAmount = 1;

        public override bool Execute(GameObject target, int amount)
        {
            return target.GetComponent<IHealthManager>().Heal(healAmount * amount);
        }
    }
}
