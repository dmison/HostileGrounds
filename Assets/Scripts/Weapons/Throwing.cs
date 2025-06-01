using UnityEngine;

namespace Weapons
{
    public class Throwing : MonoBehaviour
    {
        [SerializeField] private float throwForce = 10f;
        [SerializeField] private float forceMultiplier = 0;
        [SerializeField] private float forceMultiplierLimit = 4f;
        [SerializeField] private GameObject throwableSpawn;
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
        }

        public void Throw(GameObject throwableObject, float preparedSeconds)
        {
            // Throw Physics
            preparedSeconds = Mathf.Clamp(preparedSeconds, 0, forceMultiplierLimit);
            GameObject throwable = Instantiate(throwableObject, throwableSpawn.transform.position, Quaternion.identity);
            Rigidbody rb = throwable.GetComponent<Rigidbody>();
            rb.AddForce(_camera.transform.forward * (throwForce * preparedSeconds), ForceMode.Impulse);
            throwable.GetComponent<ThrowableGrenade>().hasBeenThrown = true;
        }

    }
}