using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameMenu : MonoBehaviour
{
    // Starts the game by loading the MainLevel scene
    public void StartGame()
    {
        SceneManager.LoadScene("MainLevel");
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
    public void GoToStartScene()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
