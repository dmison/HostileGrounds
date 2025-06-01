using UnityEngine;
using Weapons;

namespace Pickups
{
    [CreateAssetMenu(fileName = "AmmoPickup", menuName = "HostileGrounds/Pickups/AmmoPickup")]
    public class AmmoPickupData : PickupData
    {
        public int ammoAmount = 1;
        public WeaponType ammoType;

        public override bool Execute(GameObject target, int amount)
        {
            return target.GetComponent<WeaponsManager>().AddAmmo(ammoType, ammoAmount * amount);
        }
    }
}