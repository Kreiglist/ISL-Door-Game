using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text masterTimerText;
    [SerializeField] private Text ammoText;
    
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text finalScoreText;

    [SerializeField] private GameObject highscorePrefab;
    [SerializeField] private Transform highscoreFrame;
    List<GameObject> highscoreUI = new List<GameObject>();
    
    private int score;
    private float time;
    private float masterTime;
    private int ammo;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        UpdateScore();
        UpdateTimer();
        UpdateMasterTimer();
        UpdateAmmo();
        GameOver(false);
    }
    public void AddScore(int value)
    {
        score += value;
        UpdateScore();
    }
    public void SetTimer(float value)
    {
        time = value;
        UpdateTimer();
    }
    public void SetMasterTimer(float value)
    {
        masterTime = value;
        UpdateMasterTimer();
    }
    public void AmmoCount(int value)
    {
        ammo = value;
        UpdateAmmo();
    }
    public void GameOver(bool activate) 
    {
        if (activate == true) // Activate the game over screen and stop the game from running
        {
            gameOverScreen.SetActive(true);
            UpdateScore();
            Time.timeScale = 0;
        }
        else // Make sure the game over screen is off and the game and coroutines are running
        {
            Time.timeScale = 1;
            gameOverScreen.SetActive(false);
        }
    }
    public int Score
    {
        get
        {
            return score;
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
        for(int i = 0; i < list.Count; i++)
        {
            HighscoreElements he = list[i];

            if(he.highscore >= 0)
            {
                if(i >= highscoreUI.Count)
                {
                    var inst = Instantiate(highscorePrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(highscoreFrame, false);

                    highscoreUI.Add(inst);
                }
                var texts = highscoreUI[i].GetComponentsInChildren<Text>();
                texts[0].text = (i+1).ToString();
                texts[1].text = he.highscore.ToString();
            }
        }
    }

    private void UpdateScore()
    {
        if (score >= 0)
        {
            scoreText.text = "Score:" + score.ToString();
            finalScoreText.text = "Your Score: " + score.ToString();
            //HighscoreManager.highscoreManager.AddHighscore(new HighscoreElements(score));
        }
    }
    private void UpdateTimer()
    {
        // Rounds time down to the nearest whole number and converts it to an integer.
        // Extracts only the seconds part from the time variable.
        int seconds = Mathf.FloorToInt(time); 
        
        // Subtracts the whole seconds from time, leaving only the decimal part.
        // Multiplies the decimal by 100 to convert it into a two-digit millisecond value. Extracts milliseconds.
        int milliseconds = Mathf.FloorToInt((time - seconds) * 100);

        // Formats the seconds and milliseconds values into a 00:00 format.
        // - If seconds = 5, it shows 05.
        // - If milliseconds = 3, shows 03.
        timerText.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
    }
    private void UpdateMasterTimer()
    {
        int seconds = Mathf.FloorToInt(masterTime);
        int milliseconds = Mathf.FloorToInt((masterTime - seconds) * 100);
        masterTimerText.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
    }
    private void UpdateAmmo()
    {
        ammoText.text = "Ammo: " + ammo.ToString();
    }
}