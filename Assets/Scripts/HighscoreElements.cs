using System;

[Serializable]
public class HighscoreElements
{
    //public string playerRank;
    public int highscore;

    public HighscoreElements (int score)
    {
        //playerRank = rank;
        highscore = score;
    }
}
