using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        
        [SerializeField] private Throwing throwing;
        [SerializeField] private GameObject grenadePrefab; 
        private int grenades = 0;
        private DateTime _preppingThrowStartDateTime;
        private bool _preppingThrow = false;
        
        // ====================================================================================================
        private void Start()
        {
            throwing = GetComponent<Throwing>();
            
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
            grenades--;
            _preppingThrow = false;
            UpdateUI();
            if (throwing)
            {
                throwing.Throw(grenadePrefab, preparedSeconds);
            }
        }
        
        public bool AddGrenades(int numberOfGrenades)
        {
            if (grenades >= maxGrenades) return false;
            grenades += numberOfGrenades;
            grenades = Mathf.Clamp(grenades, 0, maxGrenades);
            UpdateUI();
            return true;
        }
        public bool AddAmmo(WeaponType ammoType, int amount)
        {
            // are we already maxed out on this ammo type?
            if( _magazines.ContainsKey(ammoType) && maxMagazines.Any(
                   mm => mm.ammoType == ammoType &&
                         mm.max == _magazines[ammoType].Carried ))
            {
                return false;
            }
            // try to add the new magazine
            // if will fail silently if there are already mags of that type
            Magazine mag = new Magazine();
            _magazines.TryAdd(ammoType, mag);

            // then get the magazine of that type & increment
            // the number carried by one.
            Magazine magazine = _magazines[ammoType];
            magazine.Carried += amount;
            _magazines[ammoType] = magazine;

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
        public int Carried;
    }

    [Serializable]
    public class MaxMagazinesPerType
    {
        [SerializeField] public WeaponType ammoType;
        [SerializeField] public int max;
    }
}
