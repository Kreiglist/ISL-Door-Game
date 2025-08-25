using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    
    [SerializeField] private Text scoreText;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text answeredQuestionText;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text finalScoreText;

    [SerializeField] private GameObject highscorePrefab;
    [SerializeField] private Transform highscoreFrame;
    List<GameObject> highscoreUI = new List<GameObject>();

    [SerializeField] private GameObject radialTimer;
    public bool isRadialActive;

    private int score;
    private int ammo;
    private int answerCount;
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
        UpdateAmmo();
        GameOver(false);
    }
    public void AddScore(int value)
    {
        score += value;
        UpdateScore();
    }
    public void AddQuestionCount (int value)
    {
        answerCount += value;
        UpdateAnswerCount();
    }
    private void UpdateAnswerCount()
    {
        answeredQuestionText.text = answerCount.ToString();
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
    private void UpdateAmmo()
    {
        ammoText.text = "Ammo: " + ammo.ToString();
    }
}