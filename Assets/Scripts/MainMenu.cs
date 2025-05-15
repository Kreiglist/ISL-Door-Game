using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    private void Start()
    {
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        PauseGame();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
    public void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy == false)
            {
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            }
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
}