using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{


    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public UnityEngine.UI.Slider soundSlider;
    public UnityEngine.UI.Slider musicSlider;

    void Start()
    {
        if (Data.Instance != null)
        {
            soundSlider.value = Data.Instance.sound;
            musicSlider.value = Data.Instance.music;
        }
    }

    public void SaveSettings()
    {
        if (Data.Instance != null)
        {
            Data.Instance.sound = soundSlider.value;
            Data.Instance.music = musicSlider.value;
            Data.Instance.SaveData();
        }
    }

}