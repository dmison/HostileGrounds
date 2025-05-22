using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;
 

    // Update is called once per frame
    void Update()
    {
        // move this code to player inputs later
        // left mouse click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        // shoot bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        // destroy bullet on collision


        // destroy bullet after time has passed without colliding
        StartCoroutine(DestroyBullet(bullet, bulletPrefabLifeTime));
    }

    private IEnumerator DestroyBullet (GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
