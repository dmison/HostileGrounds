using System;
using System.Collections.Generic;
using System.Linq;
using Pickups;
using Pickups.Ammo;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
    public class WeaponsManager : MonoBehaviour
    {
        private Camera playerCamera;
        
        [Header("Weapon Prefabs")]
        [SerializeField] private WeaponGun primaryWeapon;
        [SerializeField] private WeaponGun secondaryWeapon;
        
        private enum WeaponSelector { Primary, Secondary }
        private Dictionary<WeaponSelector, WeaponGun> weapons = new Dictionary<WeaponSelector, WeaponGun>();
        private Dictionary<WeaponSelector, GameObject> weaponGameObjects = new Dictionary<WeaponSelector, GameObject>();
        private WeaponSelector currentWeapon = WeaponSelector.Primary;
        
        // maximum number of magazines of each type that can be carried
        [SerializeField] private List<MaxMagazinesPerType> maxMagazines = new List<MaxMagazinesPerType>();
        [SerializeField] private int maxGrenades = 5;
        
        // magazines carried
        private Dictionary<WeaponType, Magazine> _magazines = new Dictionary<WeaponType, Magazine>();
        [SerializeField] private Transform gunHolder;
        
        // ====================================================================================================
        // Throwables
        public int grenades = 0;
        public float throwForce = 10f;
        public GameObject grenadePrefab;
        public GameObject throwableSpawn;
        public float forceMultiplier = 0;
        public float forceMultiplierLimit = 4f;
        private DateTime _preppingThrowStartDateTime;
        private bool _preppingThrow = false;
        // ====================================================================================================
        private void Start()
        {
            weapons[WeaponSelector.Primary] = primaryWeapon;
            weapons[WeaponSelector.Secondary] = secondaryWeapon;
            
            weaponGameObjects[WeaponSelector.Primary] = Instantiate(primaryWeapon.gameObject, gunHolder);
            weaponGameObjects[WeaponSelector.Secondary] = Instantiate(secondaryWeapon.gameObject, gunHolder);
            
            HoldCurrentWeapon();
            UpdateUI();
        }

        private void HoldCurrentWeapon()
        {
            weaponGameObjects[WeaponSelector.Primary].SetActive(currentWeapon == WeaponSelector.Primary);
            weaponGameObjects[WeaponSelector.Secondary].SetActive(currentWeapon == WeaponSelector.Secondary);
        }
        
        public void Shoot()
        {
            if (_preppingThrow) return;
            if (weaponGameObjects[currentWeapon].GetComponent<WeaponGun>().RoundsRemaining == 0)
            {
                Reload();
                return;
            }
            weaponGameObjects[currentWeapon].GetComponent<WeaponGun>().Shoot();
            UpdateUI();
        }

        public void SwapWeapons()
        {
            if (_preppingThrow) return;
            currentWeapon = currentWeapon == WeaponSelector.Primary ? WeaponSelector.Secondary : WeaponSelector.Primary;
            HoldCurrentWeapon();
            UpdateUI();
        }

        public void Reload()
        {
            if (_preppingThrow) return;
            if (_magazines.TryGetValue(weapons[currentWeapon].weaponType, out var magazine))
            {
                if (magazine.Carried >= 1)
                {
                    weaponGameObjects[currentWeapon].GetComponent<WeaponGun>().Reload(()=>
                    {
                        magazine.Carried--;
                        _magazines[weapons[currentWeapon].weaponType] = magazine;
                        UpdateUI();
                    });        
                };
            }
        }

        public void PrepareThrow()
        {
            if (_preppingThrow || grenades <= 0) return;
            _preppingThrow = true;
            _preppingThrowStartDateTime = DateTime.Now;
        }

        public void ReleaseThrow()
        {
            if (!_preppingThrow) return;
            float preparedSeconds = (float)(DateTime.Now - _preppingThrowStartDateTime).TotalSeconds;
            preparedSeconds = Mathf.Clamp(preparedSeconds, 0, forceMultiplierLimit);
            grenades--;
            _preppingThrow = false;
            UpdateUI();
            Throw(preparedSeconds);
        }

        public void Throw(float preparedSeconds)
        {
            // Throw Physics
            GameObject throwable = Instantiate(grenadePrefab, throwableSpawn.transform.position,
                Camera.main.transform.rotation);
            Rigidbody rb = throwable.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * (throwForce * preparedSeconds), ForceMode.Impulse);
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
                case TypeOfPickup.PistolAmmo:
                case TypeOfPickup.SMGAmmo:
                {
                    AmmoMagazineSo ammo = (AmmoMagazineSo)pickupSo.pickupData;
                    result = AddAmmo(ammo);
                    break;
                }
                case TypeOfPickup.GrenadeAmmo:
                {
                    AmmoMagazineSo ammo = (AmmoMagazineSo)pickupSo.pickupData;
                    result = AddGrenades(1);
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

        private bool AddGrenades(int numberOfGrenades)
        {
            if (grenades >= maxGrenades) return false;
            grenades += numberOfGrenades;
            grenades = Mathf.Clamp(grenades, 0, maxGrenades);
            UpdateUI();
            return true;
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

            UpdateUI();

            return true;
        }

        private void UpdateUI()
        {
            UIManager.Instance.CurrentGrenadeCount = grenades;
            
            // get magazines for current gun
            if (_magazines.TryGetValue(weapons[currentWeapon].weaponType, out var magazine))
            {
                UIManager.Instance.CurrentMagazineCount = magazine.Carried;
            }
            else UIManager.Instance.CurrentMagazineCount = 0;
            
            //
            UIManager.Instance.CurrentRoundCount =
                weaponGameObjects[currentWeapon].GetComponent<IWeapon>().RoundsRemaining;
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
