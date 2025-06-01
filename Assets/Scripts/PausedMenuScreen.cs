using UnityEngine;

public class PausedMenuScreen : MonoBehaviour
{
    // Reference to if the game paused
    public static bool gameIsPaused = false;
    // Reference the pause menu
    public GameObject pausedMenuUI;

    // Force creating the screen to be static so that it can be directly called from the static method
    private static PausedMenuScreen instance;

    void Awake()
    {
        instance = this;
    }

    // Deactivate the pause menu at the start of the game
    void Start()
    {
        pausedMenuUI.SetActive(false);
    }

    // Toggle between paused and not
    public static void TogglePause()
    {
        // Flip the boolean value of gameIsPaused
        gameIsPaused = !gameIsPaused;
        Debug.Log("game is paused? " + gameIsPaused);
        // Set the menu to be active and visible (if gameIsPaused = true) or unactive and invisible (if gameIsPaused = false)
        instance.pausedMenuUI.SetActive(gameIsPaused);
        // Set the cursor to be visible (if gameIsPaused = true) or invisble (if gameIsPaused = false)
        Cursor.visible = gameIsPaused;
        // Set the cursor to be confined to the window (if gameIsPaused = true) or set the cursor to be locked to the game (if gameIsPaused = false)
        Cursor.lockState = gameIsPaused ? CursorLockMode.Confined : CursorLockMode.Locked;
        // Pause game time (if gameIsPaused = true) or resume the time (if gameIsPaused = true)
        Time.timeScale = gameIsPaused ? 0f : 1f;
    }

    // Start the game from the beginning
    public void ExitGame()
    {
        StartGameMenu.GoToStartScene();
        gameIsPaused = false;
    }
}
