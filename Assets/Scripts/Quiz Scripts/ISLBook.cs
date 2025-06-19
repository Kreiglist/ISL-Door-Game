using UnityEngine;
using UnityEngine.UI;

public class ISLBook : MonoBehaviour
{
    [SerializeField] private Image leftPage;
    [SerializeField] private Image rightPage;
    [SerializeField] private Sprite[] pageSprites;
    [SerializeField] private AudioClip pageSFX;

    private int currentPageIndex = 0;

    public void FlipToNextPage()
    {
        AudioManager.instance.PlayAudio(pageSFX, transform, 1f);
        if (currentPageIndex < pageSprites.Length - 2)
        {
            currentPageIndex += 2;
            UpdatePages();
        }
    }

    public void FlipToPreviousPage()
    {
        AudioManager.instance.PlayAudio(pageSFX, transform, 1f);
        if (currentPageIndex > 0)
        {
            currentPageIndex -= 2;
            UpdatePages();
        }
    }

    private void UpdatePages()
    {
        leftPage.sprite = pageSprites[currentPageIndex];

        if (currentPageIndex + 1 < pageSprites.Length)
            rightPage.sprite = pageSprites[currentPageIndex + 1];
    }

    // Initialize the book with the first pages
    void Start()
    {
        UpdatePages();
    }
}
