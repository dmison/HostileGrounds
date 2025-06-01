using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Reference the pause menu
    public GameObject gameOverScreen;
    public static bool gameIsOver = false;

    // Force creating the screen to be static so that it can be directly called from the static method
    private static GameOver instance;
    
    void Awake()
    {
        instance = this;
    }

    // Deactivate the pause menu at the start of the game
    void Start()
    {
        gameOverScreen.SetActive(false);
    }

    // When the player dies and the game is over
    public static void TriggerGameOver()
    {
        gameIsOver = true;
        Debug.Log("Game Over...");
        // Set the screen to be active and visible
        instance.gameOverScreen.SetActive(true);
        // Set the cursor to be visible 
        Cursor.visible = true;
        // Set the cursor to be confined to the window 
        Cursor.lockState = CursorLockMode.Confined;
        // Stop the Game
        Time.timeScale = 0f;
    }

    // Replay the game from the start on the Main Scene
    public void Replay()
    {
        StartGameMenu.StartGame();
        gameIsOver = false;
    }

    // Start the game from the beginning
    public void ExitGame()
    {
        StartGameMenu.GoToStartScene();
        gameIsOver = false;
    }
}
