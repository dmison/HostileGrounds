using System;
using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class WeaponGun : MonoBehaviour, IWeapon
    {
        private Camera playerCamera;

        [SerializeField] private GameObject weaponGunModel;

        [SerializeField] private WeaponType weaponType;
        // shooting
        [SerializeField] private float shootingDelay = 2f;
        [SerializeField] private int roundsCapacity = 10;
        [SerializeField] private float reloadTime = 2f;
        [SerializeField] private ShootingMode currentShootingMode;
        
        // Burst gun
        [SerializeField] private int bulletsPerBurst = 3;
        [SerializeField] private int currentBurstBullets;
        // spread
        [SerializeField] private float spreadIntensity;

        // Bullet Properties
        [SerializeField] private int damageToDeal;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawn;
        [SerializeField] private float bulletVelocity = 30;
        [SerializeField] private float bulletLifeTime = 3f;

        private bool readyToShoot = true;
        private bool allowReset = true;
        private int roundsRemaining = 0;
        private bool isReloading;
        private DateTime reloadStartTime;
        
        public int RoundsRemaining => roundsRemaining;
        public bool IsReloading => isReloading;
        public WeaponType WeaponType => weaponType;
        
        private enum ShootingMode
        {
            Single, Burst, Auto
        }
        
        private void Awake()
        {
            readyToShoot = true;
            currentBurstBullets = bulletsPerBurst;
            playerCamera = Camera.main;
        }

        public void Shoot()
        {
            if (readyToShoot && !isReloading)
            {
                currentBurstBullets = bulletsPerBurst;
                ShootBullet();
            }
        }

        public void Reload(Action onComplete)
        {
            if (isReloading) return;
            isReloading = true;
            weaponGunModel.transform.Rotate(new(1,1,0), -30f);
            StartCoroutine(FinishReload(onComplete));
        }

        private IEnumerator FinishReload(Action onComplete)
        {
            yield return new WaitForSeconds(reloadTime);
            weaponGunModel.transform.Rotate(new(1,1,0), 30f);
            roundsRemaining = roundsCapacity;
            isReloading = false;
            onComplete?.Invoke();
        }

        private void OnDisable()
        {
            // if we are reloading when swapping weapons then the reload will not complete so
            // reset the model back to it's original position
            if (isReloading)
            {
                weaponGunModel.transform.Rotate(new(1,1,0), 30f);
                isReloading = false;
            }
            
        }

        private void ShootBullet()
        {
            roundsRemaining -= 1;
            readyToShoot = false;

            Vector3 shootingDirection = CalculateDirectionAndSpread();
        
            // Instantiate bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            
            // Bullet direction
            bullet.transform.forward = shootingDirection;

            // Shoot bullet
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
            Bullet bs = bullet.GetComponent<Bullet>();
            if (bs)
            {
                bs.LifeTime = bulletLifeTime;
                bs.DamageToDeal = damageToDeal;
            }

            // Check if gun has finished shooting
            if (allowReset)
            {
                Invoke(nameof(ResetShot), shootingDelay);
                allowReset = false;
            }

            // Burst Mode
            if (currentShootingMode == ShootingMode.Burst && currentBurstBullets > 1)
            {
                currentBurstBullets--;
                Invoke(nameof(ShootBullet), shootingDelay);
            }
        }

        private void ResetShot()
        {
            readyToShoot = true;
            allowReset = true;
        }

        public Vector3 CalculateDirectionAndSpread()
        {
            // Shoot at middle of screen (where a crosshair would be)
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                // Shooting at something
                targetPoint = hit.point;
            }
            else
            {
                //Shooting at air
                targetPoint = ray.GetPoint(100);
            }

            Vector3 direction = targetPoint - bulletSpawn.position;

            // Bullet spread
            float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
            float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

            // Return direction and spread
            return direction + new Vector3(x, y, 0);
        }
        
        
    }
}
