using UnityEngine;
using Weapons;

namespace Pickups
{
    [CreateAssetMenu(fileName = "GrenadePickup", menuName = "HostileGrounds/Pickups/GrenadePickup")]
    public class GrenadePickupData : PickupData
    {
        public int ammoAmount = 1;

        public override bool Execute(GameObject target, int amount)
        {
            return target.GetComponent<WeaponsManager>().AddGrenades(ammoAmount * amount);
        }
    }
}