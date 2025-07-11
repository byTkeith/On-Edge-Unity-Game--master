using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // Optional: reset time scale in case the game was paused
        Time.timeScale = 1f;

        // Load the main menu scene
        SceneManager.LoadScene("MainMenuFinal"); 
    }
    public void PauseAndReturnToMenu()
    {
        Time.timeScale = 0f; // Pause game
        SceneManager.LoadScene("MainMenu");
    }

}
