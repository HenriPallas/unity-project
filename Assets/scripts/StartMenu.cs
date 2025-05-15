using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game"); 
    }


    public void SettingsMenu()
    {
        SceneManager.LoadScene("Settings");
    }


<<<<<<< Updated upstream
=======
    public void QuitGame()
    {
        Debug.Log("Quit button pressed");
        Application.Quit();
    }


>>>>>>> Stashed changes
}
