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
        }
    }
}
