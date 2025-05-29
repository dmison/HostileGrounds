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
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        // When the player presses ESC on the keyboard
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the game is paused and showing the paused menu
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // To resume the game
    public void Resume()
    {
        // Set the menu to no longer be active or visible 
        pausedMenuUI.SetActive(false);
        // Resume game time
        Time.timeScale = 1f;
        // The game is no longer paused
        gameIsPaused = false;
        // Set the cursor to be invisible and locked to the resumed game
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // To pause the game
    void Pause()
    {
        // Set the menu to be active and visible
        pausedMenuUI.SetActive(true);
        // Pause game time
        Time.timeScale = 0f;
        // The game is paused
        gameIsPaused = true;
        // Set the cursor to be visible and unlock it for the UI interactions
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
