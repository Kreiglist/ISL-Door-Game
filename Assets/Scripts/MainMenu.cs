using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    public bool toTutorial;
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void StartTutorial()
    {
        if(toTutorial == true) SceneManager.LoadScene("Tutorial");
        else SceneManager.LoadScene("GameScene");
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void EndGameToHighscore()
    {
        QuizManager.instance.EndGame();
    }
    public void QuitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
}