using System;
using System.Collections.Generic;
using System.Linq;
using Pickups;
using Pickups.Ammo;
using UnityEngine;

namespace Weapons
{
    public class WeaponsManager : MonoBehaviour
    {
        // maximum number of magazines of each type that can be carried
        [SerializeField] private List<MaxMagazinesPerType> maxMagazines = new List<MaxMagazinesPerType>();

        // magazines carried
        private Dictionary<WeaponType, Magazine> _magazines = new Dictionary<WeaponType, Magazine>();

        // Throwables
        public int grenades = 0;
        public float throwForce = 10f;
        public GameObject grenadePrefab;
        public GameObject throwableSpawn;
        public float forceMultiplier = 0;
        public float forceMultiplierLimit = 4f;

        public void Shoot()
        {
            Debug.Log("shoot");
        }

        public void SwapWeapons()
        {
            Debug.Log("swap weapons");
        }

        public void Reload()
        {
            Debug.Log("reload");
        }

        public void Throw()
        {
            // Force of the throw
            if (Input.GetKey(KeyCode.G))
            {
                forceMultiplier += Time.deltaTime;
                if (forceMultiplier > forceMultiplierLimit)
                {
                    // Puts a limit on how much force can be used
                    forceMultiplier = forceMultiplierLimit;
                }
            }
            // Throws grenade when G key is held down
            if (Input.GetKeyUp(KeyCode.G))
            {
                if (grenades > 0)
                {
                    ThrowGrenade();
                    Debug.Log("Throw Grenade");
                }

                forceMultiplier = 0;
            }
          
        }

        public void ThrowGrenade()
        {
            // Throw Physics
            GameObject throwable = Instantiate(grenadePrefab, throwableSpawn.transform.position, Camera.main.transform.rotation);
            Rigidbody rb = throwable.GetComponent<Rigidbody>();

            rb.AddForce(Camera.main.transform.forward * (throwForce * forceMultiplier), ForceMode.Impulse);

            throwable.GetComponent<ThrowableGrenade>().hasBeenThrown = true;
        }

        public bool Pickup(PickupSo pickupSo)
        {
            bool result = false;

            switch (pickupSo.PickupType)
            {
                case TypeOfPickup.Health:
                {
                     // Just an idea ... player.GetComponent<HealthManager>().Heal(healToRestore);
                     break;
                }
                case TypeOfPickup.GrenadeAmmo:
                case TypeOfPickup.PistolAmmo:
                case TypeOfPickup.SMGAmmo:
                {
                    AmmoMagazineSo ammo = (AmmoMagazineSo)pickupSo.pickupData;
                    result = AddAmmo(ammo);
                    break;
                }

                default:
                {
                    Debug.Log("Unknown Pickup type encountered");
                    break;
                }
            }


            return result;
        }

        private bool AddAmmo(AmmoMagazineSo ammo)
        {
            // are we already maxed out on this ammo type?
            if( _magazines.ContainsKey(ammo.weaponType) && maxMagazines.Any(
                   mm => mm.ammoType == ammo.pickupType &&
                         mm.max == _magazines[ammo.weaponType].Carried ))
            {
                return false;
            }
            // try to add the new magazine
            // if will fail silently if there are already mags of that type
            Magazine mag = new Magazine(ammo.roundsPer);
            _magazines.TryAdd(ammo.weaponType, mag);

            // then get the magazine of that type & increment
            // the number carried by one.
            Magazine magazine = _magazines[ammo.weaponType];
            magazine.Carried++;
            _magazines[ammo.weaponType] = magazine;

            // Just an idea ... UIManager.instance.UpdateAmmoUI

            return true;
        }
    }

    struct Magazine
    {
        public Magazine(int rounds)
        {
            RoundsPer = rounds;
            Carried = 0;
        }
        public int RoundsPer;
        public int Carried;
    }

    [Serializable]
    public class MaxMagazinesPerType
    {
        [SerializeField] public TypeOfPickup ammoType;
        [SerializeField] public int max;
    }
}
