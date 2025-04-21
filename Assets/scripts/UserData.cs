using UnityEngine;

[System.Serializable]
public class UserData
{
    public string username;
    public int games;
    public int wins;
    public int draws;
    public int losses;
    public float sound;
    public float music;

    public UserData(Data data)
    {
        username = data.username;
        games = data.games;
        wins = data.wins;
        draws = data.draws;
        losses = data.losses;
        sound = data.sound;
        music = data.music;
    }

    public override string ToString()
    {
        return $"Username: {username}, Games: {games}, Wins: {wins}, Draws: {draws}, Losses: {losses}, Sound: {sound}, Music: {music}";
    }

}
