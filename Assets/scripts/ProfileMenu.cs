using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileMenu : MonoBehaviour
{
    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public TMP_InputField usernameText;
    public TMP_Text gamesText;
    public TMP_Text winsText;
    public TMP_Text drawsText;
    public TMP_Text lossesText;

    void Start()
    {
        if (Data.Instance != null)
        {
            usernameText.text = Data.Instance.username;
            gamesText.text = $"Games: {Data.Instance.games}";
            winsText.text = $"Wins: {Data.Instance.wins}";
            drawsText.text = $"Draws: {Data.Instance.draws}";
            lossesText.text = $"Losses: {Data.Instance.losses}";
        }
    }

    public void SaveProfile()
    {
        if (Data.Instance != null)
        {
            Data.Instance.username = usernameText.text;
            Data.Instance.SaveData();
            Debug.Log("Profile saved. New username: " + Data.Instance.username);
        }
    }
}