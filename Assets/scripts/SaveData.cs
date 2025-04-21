using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string username;
    public int games;
    public int wins;
    public int draws;
    public int losses;
    public float volume;
    public float music;

    public SaveData(Data data)
    {
        username = data.username;
        games = data.games;
        wins = data.wins;
        draws = data.draws;
        losses = data.losses;
        volume = data.volume;
        music = data.music;
    }
}
