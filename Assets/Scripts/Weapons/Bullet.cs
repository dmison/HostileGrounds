using System.Collections;
using System.Collections.Generic;
using Health;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Amount of damage that is dealt
    [SerializeField]
    private int damageToDeal;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<IHealthManager>() != null)
            {
                // If HealthManager is attached then the game object will take damage based on the amount of damageToDeal
                collision.gameObject.GetComponent<IHealthManager>().TakeDamage(damageToDeal);
                Debug.Log("Dealt damage of " + damageToDeal);
            }
            else
            {
                // If HealthManager is not attached then this is explicitly stated in the consol
                Debug.Log(collision.gameObject.name + " does not have a Health Manager component");
            }

            print("hit " + collision.gameObject.name);
            // Destroy the bullet once it has collided with the object
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Environment"))
        {
            print("hit" + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
