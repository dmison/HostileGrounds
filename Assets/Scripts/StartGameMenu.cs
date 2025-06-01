using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameMenu : MonoBehaviour
{
    // Starts the game by loading the MainLevel scene
    public static void StartGame()
    {
        SceneManager.LoadScene("MainLevel");
        Debug.Log("Starting Game...");
        Time.timeScale = 1f;
    }

    public static void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        // Quit the Game
        Application.Quit();
    }

    // Shows the StartMenu scene
    public static void GoToStartScene()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
