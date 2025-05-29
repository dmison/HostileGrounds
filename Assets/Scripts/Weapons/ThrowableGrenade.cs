using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableGrenade : MonoBehaviour
{
    [SerializeField]
    float delay = 3f;
    [SerializeField]
    float damageRadius = 20f;
    [SerializeField]
    float explosionForce = 1200f;

    float explosionCountdown;

    bool hasExploded = false;
    public bool hasBeenThrown = false;

    // Amount of damage grenades do
    [SerializeField]
    private int damageToDeal = 50;

    public enum ThrowableType
    {
        Grenade
    }

    public ThrowableType throwableType;

    public void Start()
    {
        explosionCountdown = delay;
    }

    private void Update()
    {
        // If the grenade has been thrown and the countdown ends, the grenade will explode
        if(hasBeenThrown)
        {
            explosionCountdown -= Time.deltaTime;
            if (explosionCountdown <= 0f && !hasExploded)
            {
                GrenadeGoBoom();
                hasExploded = true;
            }
        }
    }

    private void GrenadeGoBoom()
    {
        Explode();
        // destroy the grenade
        Destroy(gameObject);
    }

    private void Explode()
    {
        // Explodes the grenade
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
            }

            // If enemy is in range it takes damage
            if (objectInRange.gameObject.CompareTag("Enemy"))
            {
                if (objectInRange.gameObject.GetComponent<HealthManager>() != null)
                {
                    // If HealthManager is attached then the game object will take damage based on the amount of damageToDeal
                    objectInRange.gameObject.GetComponent<HealthManager>().TakeDamage(damageToDeal);
                    Debug.Log("Dealt damage of " + damageToDeal);
                }
                else
                {
                    // If HealthManager is not attached then this is explicitly stated in the consol
                    Debug.Log(objectInRange.gameObject.name + " does not have a Health Manager component");
                }

                print("hit " + objectInRange.gameObject.name);
            }
        }


    }
}
