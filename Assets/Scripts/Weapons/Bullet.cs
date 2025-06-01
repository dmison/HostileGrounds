using System.Collections;
using Health;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        // Amount of damage that is dealt
        private int damageToDeal;
        private float lifeTime = 5f;
        public int DamageToDeal { get => damageToDeal; set => damageToDeal = value; }
        public float LifeTime { get => lifeTime; set => lifeTime = value; }
        
        private void Start()
        {
            StartCoroutine(DestroyBullet());
        }
        private IEnumerator DestroyBullet ()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }

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
}
