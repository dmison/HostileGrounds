using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // Enemy health bar slider in the UI
    [SerializeField]
    private Slider enemyHealthSlider;

    // Reference the camera within the scene
    [SerializeField]
    private Camera playerCamera;

    // TReference to this objects movement
    [SerializeField]
    private Transform target;

    /// <summary>
    /// UpdateEnemyHealthSlider handles the value of the health bar by calculating the percentage to provide values for the slider to slide
    /// if no health bar is assigned then it is stated in the consol
    /// </summary>
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (enemyHealthSlider != null)
        {
            enemyHealthSlider.value = currentHealth / maxHealth;
        }
        else
        {
            Debug.Log("Enemy health slider is not assigned!");
        }
    }

    void Update()
    {
        // Rotate the health bar with the camera rotation
        transform.rotation = playerCamera.transform.rotation;
        // Set the healthbar to be attached to the object
        transform.position = target.position;
    }
}
