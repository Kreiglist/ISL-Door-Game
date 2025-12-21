using UnityEngine;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    
    [SerializeField] private Text scoreText;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text answeredQuestionText;

    //[SerializeField] private Text finalScoreText;

    [SerializeField] private GameObject ammoObject;

    [SerializeField] private GameObject radialTimer;
    public bool isRadialActive;

    private int score;
    private int ammo;
    private int answerCount;
    private int scoreIncrement;
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
    public void AddScore(int value, bool correct)
    {
        score += value;
        scoreIncrement = value;
        PrintScore(correct);
    }
    private void PrintScore(bool correct)
    {
        switch (correct)
        {
            case true:
                scoreText.text = "+" + scoreIncrement.ToString();
                Invoke(nameof(UpdateScore), 1.0f);
                break;
            case false:
                scoreText.text = scoreIncrement.ToString();
                Invoke(nameof(UpdateScore), 1.0f);
                break;
        }
    }
    private void UpdateScore()
    {
        if (score >= 0)
        {
            scoreText.text = "Score:" + score.ToString();
            //finalScoreText.text = "Your Score: " + score.ToString();
        }
        else if(score <= 0)
        {
            scoreText.text = "Score:" + 0;
            //finalScoreText.text = "Your Score: " + 0;
        }
    }
    public void AddQuestionCount (int value)
    {
        answerCount = value;
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
            //gameOverScreen.SetActive(true);
            //UpdateScore();
            Time.timeScale = 0;
        }
        else // Make sure the game over screen is off and the game and coroutines are running
        {
            Time.timeScale = 1;
            //gameOverScreen.SetActive(false);
        }
    }
    public int Score
    {
        get
        {
            return score;
        }
    }

    private void UpdateAmmo()
    {
        ammoText.text = "Ammo: " + ammo.ToString();
        for(int i = 0; i <= ammo; i++)
        {
            Instantiate(ammoObject);
        }
    }
}