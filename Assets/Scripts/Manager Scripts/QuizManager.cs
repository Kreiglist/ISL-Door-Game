using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;

    [SerializeField] private QuestionsScriptable questionsList; // Get the question scriptable
    [SerializeField] private Image questionImg; // The image component that will display the question images
    [SerializeField] private List<Button> options; // The options / doors

    [SerializeField] public int scoreValueCorrect = 100;
    [SerializeField] public int scoreValueIncorrect = -50;
    [SerializeField] public float masterTimeLimit;
    [SerializeField] public float timeLimit;

    private static List<Question> unansweredQuestions;
    private Question currQuestion;
    [SerializeField] private Timer timer;
    [SerializeField] private Ammunition ammo;
    private void Awake()
    {
        if (instance == null) // Instantiate the quiz manager
        {
            instance = this;
        }
        else Destroy(gameObject);

        unansweredQuestions = null; // empty the unanswered question list
    }
    private void Start()
    {
        ammo.currAmmo = ammo.maxAmmo; // Start with full ammunition
        HUDManager.Instance.AmmoCount(ammo.currAmmo); // Display the ammunition

        MovingPanel.Instance.SetTimer(timeLimit); // Set the timer's time
        timer.StartMasterTimer(masterTimeLimit); // Start the Master timer
        timer.StartTimer(timeLimit); // Start the moving panel

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questionsList.questions.ToList<Question>();
        }
        GetQuestion();
    }
    private void Update()
    {
        QuestionTimer();
    }
    
    private void QuestionTimer() // Run the moving questions and quiz timer
    {
        // Master Timer, How much time the player has to play the quiz //
        if (timer.isMasterRunning == true)
        {
            timer.RunMasterTimer();
            HUDManager.Instance.SetMasterTimer(timer.currMasterTime);

            if (timer.currMasterTime == 0)
            {
                HUDManager.Instance.GameOver(true);
            }
        }
        // Question Timer, How much time it takes for the moving question to "squish the player" causing a game over //
        if (timer.isRunning == true)
        {
            timer.RunTimer();
            HUDManager.Instance.SetTimer(timer.currTime);
            MovingPanel.Instance.MovePanel();

            if (timer.currTime == 0) // End the game when the player runs out of time
            {
                HUDManager.Instance.GameOver(true);
            }
        }
    }
    private void GetQuestion()
    {
        int randQuestionIdx = Random.Range(0, unansweredQuestions.Count);
        currQuestion = unansweredQuestions[randQuestionIdx];

        DisplayQuestion();
    }
    private void DisplayQuestion() // Display the question and its options
    {
        if (currQuestion != null)
        {
            if (currQuestion.question != null)
            {
                questionImg.sprite = currQuestion.question; // Assign the image
                questionImg.gameObject.SetActive(true); // Ensure it's visible
            }
            else
            {
                Debug.LogError("currQuestion.question is NULL! No sprite assigned.");
                questionImg.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("currQuestion is NULL! No question assigned.");
        }
        // Assign answer choices to options/doors //
        for (int i = 0; i < options.Count; i++)
        {
            if (i < currQuestion.options.Count)
            {
                options[i].GetComponentInChildren<Text>().text = currQuestion.options[i]; // Assign text
                int index = i; // Store index to avoid closure issues
                // Remove old listeners and add new one
                options[i].onClick.RemoveAllListeners();
                options[i].onClick.AddListener(() => Answer(index));
            }
            else
            {
                options[i].gameObject.SetActive(false); // Hide extra buttons if not needed
            }
        }
    }
    void OnButtonClick(Button clickedButton)
    {
        clickedButton.interactable = false; // Ensures the disabled sprite is shown
    }
    private void AnswerGun(bool correct) // Function for adding and removing bullets/lives
    {
        if (correct == true)
        {
            ammo.Gain(1);
        }
        else
        {
            ammo.Reduce(1);
        }
        // Argument for when the player runs out of bullets //
        if(ammo.currAmmo == 0)
        {
            foreach(Button btn in options)
            {
                btn.interactable = false;
            }
            HUDManager.Instance.GameOver(true); // Activate the game over screen
        }
    }
    public void Answer(int btnIndex) // Function for answering / clicking buttons (or doors)
    {
        Button clickedButton = options[btnIndex];
        OnButtonClick(clickedButton);
        // The following if-else statement is used for when the player answers correctly or incorrectly //
        if (currQuestion.answer == currQuestion.options[btnIndex]) // If the player answer correctly
        {
            StartCoroutine(CorrectAnswer(true));
            AnswerGun(true); // Set to true, in which add shells

            HUDManager.Instance.AmmoCount(ammo.currAmmo); // Update the shells counter
            HUDManager.Instance.AddScore(scoreValueCorrect); // add points

            timer.isRunning = false; // stop the moving question
            timer.isMasterRunning = false; // stop the timer
        }
        else  // If the player answer wrong
        {
            StartCoroutine(CorrectAnswer(false));
            AnswerGun(false); // Set to false, in which reduce shells

            HUDManager.Instance.AmmoCount(ammo.currAmmo); // Update the shells counter
            HUDManager.Instance.AddScore(scoreValueIncorrect); // deduct points

            options[btnIndex].interactable = false; // disable the recently clicked door/option
        }
    }
    // IENUMERATORS
    private IEnumerator CorrectAnswer(bool answer)
    {
        Gun.instance.GunAnimPlayer("Shoot");
        yield return new WaitForSeconds(Gun.instance.shootLen);

        if(answer == true)
        {
            CutsceneManager.instance.CutscenePlayer("Walk");
            yield return new WaitForSeconds(CutsceneManager.instance.walkLen);
            yield return StartCoroutine(NextQuestion());
        }
    }
    private IEnumerator NextQuestion() // IEnumerator for cycling questions
    {
        foreach (Button btn in options) // removes listeners doors for the duration
        {
            btn.onClick.RemoveAllListeners();
        }

        unansweredQuestions.Remove(currQuestion); // remove recently answered question

        if (unansweredQuestions == null || unansweredQuestions.Count == 0) // So the questions will cycle
        {
            // If there are no unanswered questions in the list, then refresh the list
            unansweredQuestions = questionsList.questions.ToList<Question>();
        }

        timer.StartTimer(timeLimit); // Restart the timer 
        timer.isMasterRunning = true;
        GetQuestion(); // Get a new question

        foreach (Button btn in options) // enable doors for the duration
        {
            btn.interactable = true;
        }

        MovingPanel.Instance.ResetPanel(); // Set the moving question back to its initial position
        CutsceneManager.instance.CutscenePlayer(default);
        Gun.instance.GunAnimPlayer(default);
        yield return null;
    }
}