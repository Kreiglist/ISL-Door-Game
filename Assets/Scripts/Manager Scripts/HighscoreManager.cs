using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    List<HighscoreElements> highscores = new List<HighscoreElements>();
    [SerializeField] int maxCount = 6;
    [SerializeField] string filename;

    public delegate void OnHighscoreListChanged(List<HighscoreElements> list);
    public static event OnHighscoreListChanged onHighscoreListChanged;

    [SerializeField] private GameObject highscorePrefab;
    [SerializeField] private Transform highscoreFrame;
    List<GameObject> highscoreUI = new List<GameObject>();

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
    private void OnEnable()
    {
        HighscoreManager.onHighscoreListChanged += UpdateHighscore;
    }
    private void OnDisable()
    {
        HighscoreManager.onHighscoreListChanged -= UpdateHighscore;
    }
    private void UpdateHighscore(List<HighscoreElements> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            HighscoreElements he = list[i];

            if (he.highscore >= 0)
            {
                if (i >= highscoreUI.Count)
                {
                    var inst = Instantiate(highscorePrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(highscoreFrame, false);

                    highscoreUI.Add(inst);
                }
                var texts = highscoreUI[i].GetComponentsInChildren<Text>();
                texts[0].text = (i + 1).ToString();
                texts[1].text = he.highscore.ToString();
            }
        }
    }
}
