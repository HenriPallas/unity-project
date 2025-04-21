using System;
using UnityEngine;

public class Data : MonoBehaviour 
{
    public static Data Instance;

    public string username = "User";
    public int games = 0;
    public int wins = 0;
    public int draws = 0;
    public int losses = 0;
    public float sound = 0.5f;
    public float music = 0.5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }

    }

    public void SaveData()
    {
        SaveSystem.SaveData(this);
        Debug.Log(this);
    }

    public void LoadData()
    {
        UserData data = SaveSystem.LoadData();

        Debug.Log(data);

        if (data != null)
        {
            username = data.username;
            games = data.games;
            wins = data.wins;
            draws = data.draws;
            losses = data.losses;
            sound = data.sound;
            music = data.music;
        }
    }
}
