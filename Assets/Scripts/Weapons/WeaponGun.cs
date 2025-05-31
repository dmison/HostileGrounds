using System;
using UnityEngine;

namespace Weapons
{
    public class WeaponGun : MonoBehaviour, IWeapon
    {
        private Camera playerCamera;

        // shooting
        public bool isShooting; 
        public bool readyToShoot = true;
        bool allowReset = true;
        public float shootingDelay = 2f;

        // Burst gun
        public int bulletsPerBurst = 3;
        public int currentBurstBullets;

        // spread
        public float spreadIntensity;

        // Bullet Properties
        public GameObject bulletPrefab;
        public Transform bulletSpawn;
        public float bulletVelocity = 30;
        public float bulletPrefabLifeTime = 3f;

        public WeaponType weaponType;
        public enum ShootingMode
        {
            Single,
            Burst,
            Auto
        }

        public ShootingMode currentShootingMode;

        private void Awake()
        {
            Debug.Log("awake");
            readyToShoot = true;
            currentBurstBullets = bulletsPerBurst;
            playerCamera = Camera.main;
        }

        // Update is called once per frame
        // void Update()
        // {
        //     // if (currentShootingMode == ShootingMode.Auto)
        //     // {
        //     //     // Hold down left mouse
        //     //     isShooting = Input.GetKey(KeyCode.Mouse0);
        //     // }
        //     // else if (currentShootingMode == ShootingMode.Burst)
        //     // {
        //     //     // Click left mouse
        //     //     isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        //     // }
        //     // else if (currentShootingMode == ShootingMode.Single)
        //     // {
        //     //     // Click left mouse
        //     //     isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        //     // }
        //
        //     // if (readyToShoot && isShooting)
        //     // {
        //     //     currentBurstBullets = bulletsPerBurst;
        //     //     Shoot();
        //     // }
        //     // move this code to player inputs later
        //     // left mouse click
        //     //if (Input.GetKeyDown(KeyCode.Mouse0))
        //     //{
        //     //    Shoot();
        //     //}
        // }

        private void Start()
        {
            playerCamera = Camera.main;
        }

        public void Shoot()
        {
            if (readyToShoot )
            {
                currentBurstBullets = bulletsPerBurst;
                InternalShoot();
            }
        }
        
        private void InternalShoot()
        {
            
            readyToShoot = false;

            Vector3 shootingDirection = CalculateDirectionAndSpread();
        
            // Instantiate bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            
            // Bullet direction
            bullet.transform.forward = shootingDirection;

            // Shoot bullet
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);

            // Destroy bullet after time has passed without colliding
            

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
                Invoke(nameof(InternalShoot), shootingDelay);
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
