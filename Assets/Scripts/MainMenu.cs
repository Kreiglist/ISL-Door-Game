using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
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
    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen.activeInHierarchy == false)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen.activeInHierarchy == true)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }
}