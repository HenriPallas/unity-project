using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject mainGame;

    public TextMeshProUGUI sound;
    public TextMeshProUGUI music;

    private bool isSoundOn = true;
    private bool isMusicOn = true;

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); 
    }


    public void SettingsMenu()
    {
        settings.SetActive(true);
        mainGame.SetActive(false);
    }

    public void BackButton()
    {
        settings.SetActive(false);
        mainGame.SetActive(true);
    }

    public void SoundButton()
    {
        isSoundOn = !isSoundOn;
        sound.text = "Sound: " + (isSoundOn ? "ON" : "OFF");
    }

    public void MusicButton()
    {
        isMusicOn = !isMusicOn;
        music.text = "Music: " + (isMusicOn ? "ON" : "OFF");
    }

    public void QuitGame()
{
    Debug.Log("Quit button pressed");
    Application.Quit();
}


}
