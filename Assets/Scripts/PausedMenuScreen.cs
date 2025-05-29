using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenuScreen : MonoBehaviour
{
    // Reference to if the game paused
    public static bool gameIsPaused = false;
    // Reference the pause menu
    public GameObject pausedMenuUI;
    
    void Start()
    {
        pausedMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // When the player presses ESC on the keyboard
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Toggle between paused and not
    public void TogglePause()
    {
        // Flip the boolean value of gameIsPaused
        gameIsPaused = !gameIsPaused;
        Debug.Log("game is paused? " + gameIsPaused);
        // Set the menu to be active and visible (if gameIsPaused = true) or unactive and invisible (if gameIsPaused = false)
        pausedMenuUI.SetActive(gameIsPaused);
        // Set the cursor to be visible (if gameIsPaused = true) or invosble (if gameIsPaused = false)
        Cursor.visible = gameIsPaused;
        // Set the cursor to be confined to the window (if gameIsPaused = true) or set the cursor to be locked to the game (if gameIsPaused = false)
        Cursor.lockState = gameIsPaused ? CursorLockMode.Confined : CursorLockMode.Locked;
        // Pause game time (if gameIsPaused = true) or resume the time (if gameIsPaused = true)
        Time.timeScale = gameIsPaused ? 0f : 1f;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        // Quit the Game
        Application.Quit();
    }
}
