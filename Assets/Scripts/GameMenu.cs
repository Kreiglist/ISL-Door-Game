using UnityEngine;
public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject bookScreen;
    [SerializeField] private AudioClip bookSFX;
    [SerializeField] private GameObject cutsceneScreen;
    private void Start()
    {
        pauseScreen.SetActive(false);
        bookScreen.SetActive(false);
        Cursor.visible = false;
    }
    private void Update()
    {
        PauseGame();
        IsBookActive();
    }
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen.activeInHierarchy == false && cutsceneScreen.activeInHierarchy == false)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen.activeInHierarchy == true)
        {
            ResumeGame();
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        Cursor.visible = false;
    }
    public void IsBookActive()
    {
        KeyCode bookKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CustomKey"));

        if (Input.GetKeyDown(bookKey) && bookScreen.activeInHierarchy == false && pauseScreen.activeInHierarchy == false)
        {
            bookScreen.SetActive(true);
            AudioManager.instance.PlayAudio(bookSFX, transform, 1f);
        }
        else if (Input.GetKeyDown(bookKey) && bookScreen.activeInHierarchy == true && pauseScreen.activeInHierarchy == false)
        {
            bookScreen.SetActive(false);
        }
    }
}