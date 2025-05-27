using System;
using UnityEngine;
using Weapons;

namespace Pickups.Ammo
{
    [Serializable]
    [CreateAssetMenu(fileName = "Magazine", menuName = "HostileGrounds/")]
    public class AmmoMagazineSo : PickupData
    {
        public WeaponType weaponType;
        public int roundsPer;
    }
}
