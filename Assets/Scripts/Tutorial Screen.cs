using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScreen : MonoBehaviour
{ 
    [SerializeField] private Image tutorialImage;
    [SerializeField] private Sprite[] tutorialSprites;
    
    private int currentTutorialIndex = 0;
    void Start()
    {
        UpdateTutorial();
    }
    public void FlipToNext()
    {
        if (currentTutorialIndex < tutorialSprites.Length - 1)
        {
            currentTutorialIndex++;
            UpdateTutorial();
        }
    }
    public void FlipToPrevious()
    {
        if (currentTutorialIndex > 1)
        {
            currentTutorialIndex--;
            UpdateTutorial();
        }
    }
    private void UpdateTutorial()
    {
        tutorialImage.sprite = tutorialSprites[currentTutorialIndex];
    }
}