using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Element References")]
    [SerializeField] private Slider sldPlayerHealth;            // Player's health bar
    [SerializeField] private TextMeshProUGUI magazineText;      // Text for the magazine amount
    [SerializeField] private TextMeshProUGUI roundText;         // Text for the current rounds in the gun
    [SerializeField] private TextMeshProUGUI grenadeCountText;  // Text for the number of grenades carried 
    
    // =================================================================================== VALUES DISPLAYED IN THE UI
    private float _currentHealthPercent = 100f;
    private int _currentRoundCount = 0;
    private int _currentMagazineCount = 0;
    private int _currentGrenadeCount = 0;
    
    // =================================================================================== PROPERTIES FOR UI VALUES
    // each of these will set the value & then update the UI to match current values
    
    public int CurrentMagazineCount
    {
        get => _currentMagazineCount;
        set {  _currentMagazineCount = value; UpdateUI(); }
    }
    
    public int CurrentGrenadeCount
    {
        get => _currentGrenadeCount;
        set {  _currentGrenadeCount = value; UpdateUI(); }
    }

    public int CurrentRoundCount
    {
        get => _currentRoundCount;
        set { _currentRoundCount = value; UpdateUI(); }
    }
    
    public float CurrentHealthPercent
    {
        get => _currentHealthPercent;
        set { _currentHealthPercent = value; UpdateUI(); }
    }
    
    // updates UI elements with current values
    private void UpdateUI()
    {
        magazineText.text = _currentMagazineCount.ToString();
        grenadeCountText.text = _currentGrenadeCount.ToString();
        roundText.text = _currentRoundCount.ToString();
        sldPlayerHealth.value = _currentHealthPercent;
    }
    
    void Awake()
    {
        // Ensure that only one UIManager exists
        if (Instance != null)
        {
            Debug.LogError("There is more than one UIManager in the scene, this will break the Singleton pattern.");
        }
        Instance = this;
        UpdateUI();
    }
}
