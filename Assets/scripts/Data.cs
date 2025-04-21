using UnityEngine;

public class Data : MonoBehaviour 
{
    public string username = "User";
    public int games = 0;
    public int wins = 0;
    public int draws = 0;
    public int losses = 0;
    public float volume = 0.5f;
    public float music = 0.5f;

    public void SaveData()
    {
        SaveSystem.SaveData(this);
    }

    public void LoadData()
    {
        SaveData data = SaveSystem.LoadData();

        username = data.username;
        games = data.games;
        wins = data.wins;
        draws = data.draws;
        losses = data.losses;
        volume = data.volume;
        music = data.music;
    }
}
