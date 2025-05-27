using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Health Bar Reference")]
    // Player's health bar
    [SerializeField]
    private Slider sldPlayerHealth;

    [Header("Ammo Text References")]
    // Text for the magazine amount
    [SerializeField]
    private TextMeshProUGUI magazineText;
    // Text for the current round in the gun
    [SerializeField]
    private TextMeshProUGUI roundText;

    [Header("Ammo Starting Values")]
    // Starting magazine amounts
    [SerializeField] 
    private int magazineAmount = 10;
    // Starting ammo in the current round
    [SerializeField] 
    private int currentRound = 5;
    // Magazine size
    [SerializeField]
    private int magazineSizeLimit = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that only one UIManager exists
        if (instance != null)
        {
            Debug.LogError("There is more than one UIManager in the scene, this will break the Singleton pattern.");
        }
        instance = this;
        // Set the initial ammo values to display in the UI
        UpdateAmmoUI();
    }

    // Update the players health bar so that it changes based on damage taken and healing pickups
    public void UpdatePlayerHealthSlider(float percentage)
    {
        sldPlayerHealth.value = percentage;
    }

    // When the player reloads
    public void Reload()
    {
        // Calculate how many bullets we be reloaded into the gun
        int magazineSize = magazineSizeLimit - currentRound;
        // If there are enough bullets in the magazine to fully reload
        if (magazineAmount >= magazineSize)
        {
            // Decrease the magazineSize by the magazineAmount to reflect a reload
            magazineAmount -= magazineSize;
            // Increase the currentRound by the magazineSize as if changing magazines
            currentRound += magazineSize;
        }
        else
        {
            // If there is not enough ammo in the magazine for a full reload
            // Add the remaining amount in the magazine to the currentRound
            currentRound += magazineAmount;
            magazineAmount = 0;
        }
        Debug.Log("Reloaded");
        UpdateAmmoUI();
    }

    // When the player shoots a bullet
    public void RoundShot()
    {
        if (currentRound > 0)
        {
            // Minus the current round in the gun by 1 and update the UI
            currentRound--;
            UpdateAmmoUI();
        }
        else
        {
            // If there are no more bullets in the current round, reload the gun
            Reload();
        }
    }

    // Update the UI to reflect the current ammo values
    private void UpdateAmmoUI()
    {
        // Update the current round text in the UI
        roundText.text = currentRound.ToString();
        // Update the magazine round text in the UI
        magazineText.text = magazineAmount.ToString();
    }

    // Adds ammo to the players magazine
    public void AddMagazineAmmo (int amount)
    {
        // Add the amount picked up to the magazine
        magazineAmount += amount;
        // Update the UI to reflect this change
        UpdateAmmoUI();
    }
}
