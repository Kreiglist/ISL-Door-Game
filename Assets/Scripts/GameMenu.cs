using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject bookScreen;
    [SerializeField] private AudioClip bookSFX;
    private void Start()
    {
        pauseScreen.SetActive(false);
        bookScreen.SetActive(false);
    }
    private void Update()
    {
        PauseGame();
        IsBookActive();
    }
    public void PauseGame()
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
    public void IsBookActive()
    {
        if (Input.GetKeyDown(KeyCode.E) && bookScreen.activeInHierarchy == false)
        {
            bookScreen.SetActive(true);
            AudioManager.instance.PlayAudio(bookSFX, transform);
        }
        else if (Input.GetKeyDown(KeyCode.E) && bookScreen.activeInHierarchy == true)
        {
            bookScreen.SetActive(false);
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
}
