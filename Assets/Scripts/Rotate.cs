using UnityEngine;

public class Rotate : MonoBehaviour
{
    // How fast the object will rotate in a circle
    [SerializeField]
    private float rotationSpeed = 100f;

    // How fast the object will move up and down
    [SerializeField]
    private float hoverSpeed = 2f;

    // How high the object will move up and down
    [SerializeField]
    private float hoverHeight = 0.1f;

    // Inital starting position of the object
    private Vector3 position;

    private void Start()
    {
        // Get the starting position of the object
        position = transform.position;
    }
    // Use this for initialization
    private void Update()
    {
        // Rotate around the y-axis at a speed set buy rotationSpeed
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        // Make the object hover up and down smoothly by using a sine wave
        float newY = position.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(position.x, newY, position.z);
    }
}
