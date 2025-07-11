using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource menuMusic;
    public void StartGame()
    {
        if (menuMusic != null)
        {
            menuMusic.Stop(); 
        }
        SceneManager.LoadScene("Sandbox"); 
    }

    public void QuitGame()
    {
        //Debug.Log("Quit!");
        Application.Quit();
    }
}
