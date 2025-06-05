using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    List<HighscoreElements> highscores = new List<HighscoreElements>();
    [SerializeField] int maxCount = 6;
    [SerializeField] string filename;

    public delegate void OnHighscoreListChanged(List<HighscoreElements> list);
    public static event OnHighscoreListChanged onHighscoreListChanged;

    public static HighscoreManager highscoreManager;
    private void Awake()
    {
        if (highscoreManager == null)
        {
            highscoreManager = this;
        }
        else if (highscoreManager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadHighscores();
    }
    private void LoadHighscores()
    {
        highscores = FileHandler.ReadListFromJSON<HighscoreElements>(filename);

        while(highscores.Count > maxCount)
        {
            highscores.RemoveAt(maxCount);
        }
        if(onHighscoreListChanged != null)
        {
            onHighscoreListChanged.Invoke(highscores);
        }
    }
    private void SaveHighScore()
    {
        FileHandler.SaveToJSON<HighscoreElements>(highscores, filename);
    }
    public void AddHighscore(HighscoreElements elements) 
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (i >= highscores.Count || elements.highscore > highscores[i].highscore)
            {
                highscores.Insert(i, elements);

                while (highscores.Count > maxCount)
                {
                    highscores.RemoveAt(maxCount);
                }
                SaveHighScore();
                if (onHighscoreListChanged != null)
                {
                    onHighscoreListChanged.Invoke(highscores);
                }
                break;
            }
        }
    }
}
