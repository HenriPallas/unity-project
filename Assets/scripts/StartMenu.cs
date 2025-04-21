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

    public void SensorsMenu()
    {
        SceneManager.LoadScene("Sensors");
    }

    public void ProfileMenu()
    {
        SceneManager.LoadScene("Profile");
    }

}
